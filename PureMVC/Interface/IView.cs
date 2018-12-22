using System;



namespace PureMVC
{
    public interface IView
    {
        void RegisterObserver(int observeKey, IObserverBase observer);
        bool HasObserver(int observeKey);
        void NotifyObservers<T>(INotification<T> noti);
        void NotifyObservers(INotification noti);
        void RemoveObserver(int observeKey, object notifyContext);

        void RegisterMediator<T>(IMediator<T> mediator);
        void RemoveMediator<T>(IMediator<T> mediator);
        void RegisterMediator(IMediator mediator);
        void RemoveMediator(IMediator mediator);
    }
}