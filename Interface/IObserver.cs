using System;
namespace UnityEngine
{
    public interface IObserverBase
    {
        object NotifyContext { get; set; }
        bool CompareNotifyContext(object obj);
    }
    public interface IObserver<T>:IObserverBase
    {
        Events.UnityAction<INotification<T>> NotifyMethod { get; set; }
        void NotifyObserver(INotification<T> notification);

    }
    public interface IObserver : IObserverBase
    {
        Events.UnityAction<INotification> NotifyMethod { get; set; }
        void NotifyObserver(INotification notification);
        
    }
}
