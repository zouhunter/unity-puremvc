using System;
using UnityEngine;
using System.Collections.Generic;
public class Notifyer : INotifier
{
    private IView m_view;
    private IEventHolder m_eventHolder;
    public Notifyer()
    {
        m_view = View.Instance;
        m_eventHolder = EventHolder.Instance;
    }

    /// <summary>
    /// 通知观察者
    /// </summary>
    /// <param name="notification"></param>
	private void NotifyObservers<T>(INotification<T> notification)
    {
        m_view.NotifyObservers<T>(notification);
        m_eventHolder.NotifyObserver<T>(notification);
    }
    public void SendNotification(string observeName)
    {
        SendNotification<object>(observeName, null,null);
    }
    public void SendNotification<T>(string observeName, T body)
    {
        SendNotification<T>(observeName, body,null);
    }
    public void SendNotification<T>(string observeName, T body, Type type)
    {
        Notification<T> notify = Notification<T>.Allocate(observeName, body, type);
        NotifyObservers(notify);
        notify.Release();
    }
}
