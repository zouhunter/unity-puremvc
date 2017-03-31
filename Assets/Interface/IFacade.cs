using UnityEngine;
using System;
using UnityEngine.Events;
namespace PureMVC.Internal
{

    public interface IFacade : INotifier
    {
        void RegisterProxy(IProxy prox);
        void CansaleRetrieve(string name);
        void RetrieveProxy<T>(string name, UnityAction<T> onRetieved) where T : IProxy;
        IProxy RemoveProxy(string name);

        void RegisterMediator(IMediator mediator);
        void RemoveMediator(IMediator name);

        void RegisterCommand<T>(string noti) where T : ICommand, new();
        void RemoveCommand(string noti);
    }
}