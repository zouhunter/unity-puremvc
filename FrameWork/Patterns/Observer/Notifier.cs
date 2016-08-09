using UnityEngine;
using System;
public class Notifier : INotifier
{
    public virtual void SendNotification(NotiConst notificationName)
    {
        m_facade.SendNotification(notificationName);
    }

    public virtual void SendNotification(NotiConst notificationName, object body)
    {
        m_facade.SendNotification(notificationName, body);
    }

    public virtual void SendNotification(NotiConst notificationName, object body, Type type)
    {
        m_facade.SendNotification(notificationName, body, type);
    }
    private IFacade m_facade;
    void Start(){
        m_facade = Facade.Instance;
    }
}
