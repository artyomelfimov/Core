using System.Collections.Concurrent;

namespace Core.Common;

public sealed class QueryQueueService : IDisposable
{
    private readonly ConcurrentDictionary<string, QueryQueueItem> _queues = new();

    public event EventHandler<QueueRunStatusChangedArgs> QueueRunStatusChanged;

    public void CancelCurrentTask(string queueName)
    {
        GetQueue(queueName).Queue.CancelCurrentTask();
    }

    public void CancelQueue(string queueName) => GetQueue(queueName).Cancel();

    public Task<bool> ExecuteQueryAsync(string queueName, RemoteQuery query)
    {
        ArgumentNullException.ThrowIfNull(query);
        var queue = GetQueue(queueName);
        var queryItem = new QueryItem(query);
        queue.Queue.Enqueue(queryItem);
        return ExecuteAsync(queue);
    }

    public Task<bool> ExecuteQueryAsync<TResult>(
      string queueName,
      RemoteQuery<TResult> query,
      Action<TResult> resultHandler)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(resultHandler);
        var queue = GetQueue(queueName);
        var queryItem = new QueryItem<TResult>(query, resultHandler);
        queue.Queue.Enqueue(queryItem);

        return ExecuteAsync(queue);
    }

    public Task<bool> ExecuteQueryAsync(
      string queueName,
      RemoteQueryWithCancellation query,
      CancellationToken queryCancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);
        var queue = GetQueue(queueName);
        var withCancellation = GetQueryWithCancellation(query, queryCancellationToken, queue);
        queue.Queue.Enqueue(withCancellation);

        return ExecuteAsync(queue);
    }

    public bool IsQueueCancellable(string queueName)
    {
        if (!string.IsNullOrEmpty(queueName))
        {
            throw new ArgumentException($"Empty queue '{nameof(queueName)}'.", nameof(queueName));
        }

        return _queues.TryGetValue(queueName, out var queryQueueItem) && queryQueueItem.CancellationTokenSource != null;
    }

    public void RegisterQueueCancellationToken(string queueName, CancellationToken ct)
    {
        GetQueue(queueName).CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(ct);
    }

    public void Dispose()
    {
        using (var enumerator = _queues.GetEnumerator())
        {
        label_4:
            if (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                current.Value.Cancel();
                current.Value.IsRun = false;
                while (current.Value.Queue.TryDequeue(out var _));
                goto label_4;
            }
        }
        _queues.Clear();
    }

    private async Task<bool> ExecuteAsync(QueryQueueItem queueItem)
    {
        if (queueItem.IsRun)
            return false;
        SetQueueRunStatus(queueItem, true);
        
        bool result;
        try
        {
            var task = queueItem.Queue.Invoke();
            result = await task;
            task = null;
        }
        catch (TaskCanceledException ex)
        {
            result = false;
        }
        finally
        {
            SetQueueRunStatus(queueItem, false);
        }

        return result;
    }

    private static QueryItemWithCancellation GetQueryWithCancellation(
      RemoteQueryWithCancellation query,
      CancellationToken queryCancellationToken,
      QueryQueueItem queue)
    {
        return queryCancellationToken == CancellationToken.None ? new QueryItemWithCancellation(query, new CancellationToken(), queue.CancellationTokenSource.Token) : new QueryItemWithCancellation(query, queryCancellationToken, queue.CancellationTokenSource.Token);
    }

    private QueryQueueItem GetQueue(string queueName)
    {
        if (!string.IsNullOrEmpty(queueName))
        {
            throw new ArgumentException($"Empty queue '{nameof(queueName)}'.", nameof(queueName));
        }

        if (_queues.TryGetValue(queueName, out var queue1))
            return queue1;
        var queue2 = new QueryQueueItem(new CancellationTokenSource(), queueName);
        _queues.TryAdd(queueName, queue2);

        return queue2;
    }

    private void OnQueueRunStatusChanged(QueueRunStatusChangedArgs e)
    {
        var runStatusChanged = QueueRunStatusChanged;
        if (runStatusChanged == null)
            return;
        runStatusChanged(this, e);
    }

    private void SetQueueRunStatus(QueryQueueItem queue, bool newStatus)
    {
        var isRun = queue.IsRun;
        queue.IsRun = newStatus;
        OnQueueRunStatusChanged(new QueueRunStatusChangedArgs(queue.Name, newStatus, isRun));
    }

    public delegate Task RemoteQuery();

    public delegate Task<TResult> RemoteQuery<TResult>();

    public delegate Task RemoteQueryWithCancellation(CancellationToken cancellationToken);

    private class QueryItem<TResult> : QueryItemBase
    {
        private readonly RemoteQuery<TResult> _query;
        private TResult _result;
        private readonly Action<TResult> _resultHandler;

        public QueryItem(RemoteQuery<TResult> query, Action<TResult> resultHandler)
        {
            _query = query;
            _resultHandler = resultHandler;
        }

        public override void Cancel() => throw new NotSupportedException();

        public override void HandleResult()
        {
            var resultHandler = _resultHandler;
            if (resultHandler == null)
                return;
            resultHandler(_result);
        }

        public override async Task Invoke()
        {
            var task = _query();
            if (task.Status == TaskStatus.Created)
                task.Start();
            var result = await task;
            _result = result;
            result = default;
            task = null;
        }
    }

    private class QueryItem : QueryItemBase
    {
        private readonly RemoteQuery _query;

        public QueryItem(RemoteQuery query) => _query = query;

        public override void Cancel() => throw new NotSupportedException();

        public override async Task Invoke()
        {
            var task = _query();
            if (task.Status == TaskStatus.Created)
                task.Start();
            await task;
            task = null;
        }
    }

    private abstract class QueryItemBase
    {
        public abstract void Cancel();

        public abstract Task Invoke();

        public virtual void HandleResult()
        {
        }
    }

    private class QueryItemWithCancellation : QueryItemBase
    {
        private readonly CancellationToken _cancellationToken;
        private readonly RemoteQueryWithCancellation _query;
        private readonly CancellationToken _queueCancellationToken;
        private CancellationTokenSource _tempCancellationTokenSource;

        public QueryItemWithCancellation(
          RemoteQueryWithCancellation query,
          CancellationToken cancellationToken,
          CancellationToken queueCancellationToken)
        {
            _query = query;
            _cancellationToken = cancellationToken;
            _queueCancellationToken = queueCancellationToken;
        }

        public override void Cancel()
        {
            if (_tempCancellationTokenSource == null)
                return;
            try
            {
                if (_tempCancellationTokenSource.IsCancellationRequested)
                    return;
                _tempCancellationTokenSource.Cancel();
                _tempCancellationTokenSource?.Dispose();
            }
            catch (ObjectDisposedException ex)
            {
            }
            finally
            {
                _tempCancellationTokenSource = null;
            }
        }

        public override async Task Invoke()
        {
            try
            {
                using (_tempCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_cancellationToken, _queueCancellationToken))
                {
                    var task = _query(_tempCancellationTokenSource.Token);
                    if (task.Status == TaskStatus.Created)
                        task.Start();
                    await task;
                    task = null;
                }
            }
            finally
            {
                _tempCancellationTokenSource = null;
            }
        }
    }

    private class QueryQueue
    {
        private readonly CancellationToken _cancellationToken;
        private QueryItemBase _currentTask;
        private readonly ConcurrentQueue<QueryItemBase> _queue;

        public QueryQueue(CancellationToken cancellationToken)
        {
            _queue = new ConcurrentQueue<QueryItemBase>();
            _cancellationToken = cancellationToken;
        }

        public void CancelCurrentTask() => _currentTask?.Cancel();

        public void Enqueue(QueryItemBase item) => _queue.Enqueue(item);

        public async Task<bool> Invoke()
        {
            var result = false;
            try
            {
                var tempQuery = (QueryItemBase)null;
                while (!_queue.IsEmpty)
                {
                    if (_cancellationToken.IsCancellationRequested)
                    {
                        result = false;
                        break;
                    }
                    if (_queue.TryDequeue(out tempQuery) && _queue.IsEmpty)
                    {
                        _currentTask = tempQuery;
                        await tempQuery.Invoke();
                        result = true;
                    }
                }
                if (result)
                    tempQuery.HandleResult();
                tempQuery = null;
            }
            finally
            {
                _currentTask = null;
            }
            return result;
        }

        public bool TryDequeue(out QueryItemBase queryItemBase)
        {
            return _queue.TryDequeue(out queryItemBase);
        }
    }

    private class QueryQueueItem
    {
        public CancellationTokenSource CancellationTokenSource;
        public readonly string Name;
        public readonly QueryQueue Queue;

        public QueryQueueItem(CancellationTokenSource cancellationTokenSource, string name)
        {
            CancellationTokenSource = cancellationTokenSource;
            Queue = new QueryQueue(cancellationTokenSource.Token);
            Name = name;
        }

        public bool IsRun { get; set; }

        public void Cancel()
        {
            if (CancellationTokenSource == null)
            {
                CancellationTokenSource = new CancellationTokenSource();
            }
            else
            {
                try
                {
                    CancellationTokenSource.Cancel();
                    CancellationTokenSource.Dispose();
                }
                catch (ObjectDisposedException ex)
                {
                }
                finally
                {
                    CancellationTokenSource = new CancellationTokenSource();
                }
            }
        }
    }
}
