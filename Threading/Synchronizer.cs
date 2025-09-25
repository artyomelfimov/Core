using System.Collections.Concurrent;

namespace Core.Threading;

public static class Synchronizer
{
    private static readonly ReaderWriterLockSlim LockChangeObject = new();
    private static readonly ConcurrentDictionary<string, ReaderWriterLockSlim> Locks = new ConcurrentDictionary<string, ReaderWriterLockSlim>();

    public static T ReadLockInvoke<T>(Func<T> action, string key)
    {
        var orAdd = Locks.GetOrAdd(key, s => new ReaderWriterLockSlim());
        orAdd.EnterReadLock();
        try
        {
            return action();
        }
        finally
        {
            orAdd.ExitReadLock();
        }
    }

    public static void ReadLockInvoke(Action action, string key)
    {
        var orAdd = Locks.GetOrAdd(key, s => new ReaderWriterLockSlim());
        orAdd.EnterReadLock();
        try
        {
            action();
        }
        finally
        {
            orAdd.ExitReadLock();
        }
    }

    public static T ReadLockInvoke<T>(Func<T> action)
    {
        LockChangeObject.EnterReadLock();
        try
        {
            return action();
        }
        finally
        {
            LockChangeObject.ExitReadLock();
        }
    }

    public static void ReadLockInvoke(Action action)
    {
        LockChangeObject.EnterReadLock();
        try
        {
            action();
        }
        finally
        {
            LockChangeObject.ExitReadLock();
        }
    }

    public static void WriteLockInvoke(Action action, string key)
    {
        var orAdd = Locks.GetOrAdd(key, s => new ReaderWriterLockSlim());
        orAdd.EnterWriteLock();
        try
        {
            action();
        }
        finally
        {
            orAdd.ExitWriteLock();
        }
    }

    public static T WriteLockInvoke<T>(Func<T> action, string key)
    {
        var orAdd = Locks.GetOrAdd(key, s => new ReaderWriterLockSlim());
        orAdd.EnterWriteLock();
        try
        {
            return action();
        }
        finally
        {
            orAdd.ExitWriteLock();
        }
    }

    public static T WriteLockInvoke<T>(Func<T> action)
    {
        LockChangeObject.EnterWriteLock();
        try
        {
            return action();
        }
        finally
        {
            LockChangeObject.ExitWriteLock();
        }
    }

    public static void WriteLockInvoke(Action action)
    {
        LockChangeObject.EnterWriteLock();
        try
        {
            action();
        }
        finally
        {
            LockChangeObject.ExitWriteLock();
        }
    }
}
