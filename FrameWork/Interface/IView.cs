using UnityEngine;
using System.Collections;
using System;

public interface IView {
    void RegisterObserver(NotiConst noti, IObserver observer);
    void NotifyObservers(INotification noti);
    void RemoveObserver(NotiConst notificationName, object notifyContext);

    void RegisterMediator(IMediator type);
    IMediator RetrieveMediator(string name);
    T RetrieveMediator<T>(string Name) where T :IMediator;
    IMediator RemoveMediator(string Name);

    bool HasMediator(string mediatorName);
}
