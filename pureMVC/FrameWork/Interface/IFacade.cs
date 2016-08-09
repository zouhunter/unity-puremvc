using UnityEngine;
using System;

public interface IFacade : INotifier {
    void RegisterProxy(IProxy prox);
    IProxy RetrieveProxy(string name);
    void RemoveProxy(string name);

    void RegisterMediator(IMediator mediator);
    IMediator RetrieveMediator(string name);
    T RetrieveMediator<T>(string name) where T : IMediator;
    void RemoveMediator(string name);

    void RegisterCommand(NotiConst noti,Type cmd);
    void RemoveCommand(NotiConst noti);
}
