namespace Core.Threading;

public class RepeaterFailedArgs : EventArgs
{
    public RepeaterFailedArgs(Exception exception) => Exception = exception;

    public Exception Exception { get; }
}
