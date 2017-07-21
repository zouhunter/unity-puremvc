using System;
using UnityEngine.Events;
namespace UnityEngine
{

    public interface IView
    {
        void RegisterObserver(string observerName, IObserverBase observer);
        bool HasObserver(string observerName);
        void NotifyObservers<T>(INotification<T> noti);
        void NotifyObservers(INotification noti);
        void RemoveObserver(string observerName, object notifyContext);

        void RegisterMediator<T>(IMediator<T> mediator);
        void RemoveMediator(IMediator mediator);
    }
}
