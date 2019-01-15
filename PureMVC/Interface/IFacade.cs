using System;


namespace PureMVC
{
    public interface IFacade
    {
        void RegisterProxy<T>(IProxy<T> prox);
        void RegisterProxy<T>(int proxyKey, T data);
        P RetrieveProxy<P, T>(int name) where P : IProxy<T>;
        void RetrieveProxy<P, T>(int name, Action<P> onRetieved) where P : IProxy<T>;
        void RetrieveProxy<T>(int name, Action<IProxy<T>> onRetieved);
        IProxy<T> RetrieveProxy<T>(int name);
        void RetrieveData<T>(int name, Action<T> onRetieved);
        T RetrieveData<T>(int name);
        bool HaveProxy(int name);
        void RemoveProxy(int name);
        void RegisterMediator<T>(IMediator<T> mediator);
        void RegisterMediator(IMediator mediator);
        void RemoveMediator(IMediator mediator);
        void RemoveMediator<T>(IMediator<T> mediator);
        void RegisterCommand<T, P>(int observeKey) where T : ICommand<P>, new();
        void RegisterCommand<T>(int observeKey) where T : ICommand, new();
        bool HaveCommand(int observerKey);
        void RemoveCommand(int observeKey);

       void CansaleRetrieve(int name);
        void SendNotification(int notificationName);
        void SendNotification<T>(int notificationName, T body);
    }
}