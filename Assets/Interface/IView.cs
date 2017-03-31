using System;
using UnityEngine.Events;
namespace PureMVC.Internal
{

    public interface IView
    {
        void RegisterObserver(string observerName, IObserver observer);
        bool HasObserver(string observerName);
        void NotifyObservers<T>(INotification<T> noti);
        void RemoveObserver(string observerName, object notifyContext);

        void RegisterMediator(IMediator mediator);
        void RemoveMediator(IMediator mediator);
    }
}
