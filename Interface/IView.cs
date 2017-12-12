using System;
using UnityEngine.Events;


    public interface IView
    {
        void RegisterObserver(string observerName, IObserverBase observer);
        bool HasObserver(string observerName);
        void NotifyObservers<T>(INotification<T> noti);
        void NotifyObservers(INotification noti);
        void RemoveObserver(string observerName, object notifyContext);

        void RegisterMediator<T>(IMediator<T> mediator);
        void RemoveMediator<T>(IMediator<T> mediator);
        void RegisterMediator(IMediator mediator);
        void RemoveMediator(IMediator mediator);
    }
