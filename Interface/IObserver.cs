using System;


    public interface IObserverBase
    {
        object NotifyContext { get; set; }
        bool CompareNotifyContext(object obj);
    }
    public interface IObserver<T>:IObserverBase
    {
        Action<INotification<T>> NotifyMethod { get; set; }
        void NotifyObserver(INotification<T> notification);

    }
    public interface IObserver : IObserverBase
    {
        Action<INotification> NotifyMethod { get; set; }
        void NotifyObserver(INotification notification);
        
    }
