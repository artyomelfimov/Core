namespace Core.Common;

public class QueueRunStatusChangedArgs : EventArgs
{
    public QueueRunStatusChangedArgs(string queueName, bool newValue, bool oldValue)
    {
        QueueName = queueName;
        NewValue = newValue;
        OldValue = oldValue;
    }

    public bool NewValue { get; }

    public bool OldValue { get; }

    public string QueueName { get; }
}
