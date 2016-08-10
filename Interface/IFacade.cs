using UnityEngine;
using System;
using PureMVC.Interfaces;

namespace PureMVC.Interfaces
{
    public interface IFacade : INotifier
    {
        void RegisterProxy(IProxy prox);
        T RetrieveProxy<T>(string name) where T : IProxy;
        IProxy RemoveProxy(string name);

        void RegisterMediator(IMediatorIner mediator);
        T RetrieveMediator<T>(string name) where T : IMediatorIner;
        void RemoveMediator(string name);

        void RegisterCommand<T>(string noti) where T : ICommand, new();
        ICommand RemoveCommand(string noti);
        bool HasCommand(string notificationName);

        void NotifyObservers<T>(INotification<T> notification);
    }

}
