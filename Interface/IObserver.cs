using System;
using UnityEngine;
using UnityEngine.Events;
    public interface IObserverBase
    {
        object NotifyContext { get; set; }
        bool CompareNotifyContext(object obj);
    }
    public interface IObserver<T>:IObserverBase
    {
        UnityAction<INotification<T>> NotifyMethod { get; set; }
        void NotifyObserver(INotification<T> notification);

    }
    public interface IObserver : IObserverBase
    {
        UnityAction<INotification> NotifyMethod { get; set; }
        void NotifyObserver(INotification notification);
        
    }
