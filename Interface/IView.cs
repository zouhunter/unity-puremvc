using System;
namespace PureMVC.Interfaces
{
    public interface IView
    {
        void RegisterObserver(string observerName, IObserver observer);
        void NotifyObservers<T>(INotification<T> noti);
        void RemoveObserver(string observerName, object notifyContext);

        void RegisterMediator(IMediatorIner mediator);
        T RetrieveMediator<T>(string mediatorName) where T : IMediatorIner;
        IMediatorIner RemoveMediator(string mediatorName);

        bool HasMediator(string mediatorName);
    }
}
