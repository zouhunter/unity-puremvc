using System;



namespace PureMVC
{
    public interface IView
    {
        void RegisterObserver(int observerName, IObserverBase observer);
        bool HasObserver(int observerName);
        void NotifyObservers<T>(INotification<T> noti);
        void NotifyObservers(INotification noti);
        void RemoveObserver(int observerName, object notifyContext);

        void RegisterMediator<T>(IMediator<T> mediator);
        void RemoveMediator<T>(IMediator<T> mediator);
        void RegisterMediator(IMediator mediator);
        void RemoveMediator(IMediator mediator);
    }
}