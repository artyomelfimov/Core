using Core.Properties;

namespace Core.Threading;

public class AsyncRepeater
{
    private readonly CancellationTokenSource _cts = new();
    private readonly ManualResetEvent _ewhExit = new(false);
    private readonly TimeSpan _interval;
    private bool _started;

    public event EventHandler<RepeaterFailedArgs> OperationFailed;

    public AsyncRepeater(TimeSpan interval) => _interval = interval;

    public Func<CancellationToken, Task> Operation { get; set; }

    public async Task StartAsync()
    {
        try
        {
            if (Operation == null)
            {
                throw new InvalidOperationException(Resources.StrError_OperationIsNotSetted);
            }

            if (_started)
                return;

            _started = true;
            _ewhExit.Reset();
            var token = _cts.Token;

            do
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    await Operation(token);
                    _ewhExit.Set();
                }
                catch (OperationCanceledException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    OnOperationFailed(new RepeaterFailedArgs(ex));
                }
            }
            while (!_ewhExit.WaitOne(_interval));
            token = new CancellationToken();
        }
        finally
        {
            _started = false;
        }
    }

    public void Stop() => _cts.Cancel();

    private void OnOperationFailed(RepeaterFailedArgs e)
    {
        var operationFailed = OperationFailed;
        if (operationFailed == null)
            return;
        operationFailed(this, e);
    }
}
