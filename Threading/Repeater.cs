namespace Core.Threading;

public class Repeater : IDisposable
{
    private readonly ManualResetEvent _evhExit = new(false);
    private readonly TimeSpan _repeatInterval;
    private bool _started;
    private readonly Func<Task> _task;
    private Thread _thread;

    public Repeater(Func<Task> task, TimeSpan repeatInterval)
    {
        _repeatInterval = repeatInterval;
        _task = task;
    }

    public string Name { get; set; }

    public void Start()
    {
        if (_started)
            return;
        _evhExit.Reset();
        _started = true;
        _thread = new Thread(new ParameterizedThreadStart(Execute))
        {
            Name = "Repeater: " + Name,
            IsBackground = true
        };
        _thread.Start(_task);
    }

    public void Stop()
    {
        if (!_started || Try(new Action(InnerStop)) != null)
            return;
        _started = false;
    }

    protected virtual void Execute(object taskObject)
    {
        var func = (Func<Task>)taskObject;
        do
        {
            if (_started)
                func().Wait();
        }
        while (!_evhExit.WaitOne(_repeatInterval));
    }

    public void Dispose() => _evhExit.Set();

    private void InnerStop() => _evhExit.Set();

    private static Exception Try(Action action)
    {
        try
        {
            action();
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
