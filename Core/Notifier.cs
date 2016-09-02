using System;
using UnityEngine;
using System.Collections.Generic;
public class Notifyer : INotifier
{
    private IView m_view;
    public Notifyer()
    {
        m_view = View.Instance;
    }

    private List<INotification> noitfications = new List<INotification>();

    /// <summary>
    /// 通知观察者
    /// </summary>
    /// <param name="notification"></param>
	private void NotifyObservers<T>(INotification<T> notification)
    {
        m_view.NotifyObservers<T>(notification);
    }
    public void SendNotification(string observeName)
    {
        Notification<object> notify = GetNotification<object>(observeName, null, null);

        NotifyObservers(notify);
        notify.isUsing = false;
    }
    public void SendNotification<T>(string observeName, T body)
    {
        Notification<T> notify = GetNotification<T>(observeName, body,null);

        NotifyObservers(notify);
        notify.isUsing = false;

    }
    public void SendNotification<T>(string observeName, T body, Type type)
    {
        Notification<T> notify = GetNotification<T>(observeName, body, type);

        NotifyObservers(new Notification<T>(observeName, body, type));
        notify.isUsing = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="observerName"></param>
    /// <param name="body"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private Notification<T> GetNotification<T>(string observerName, T body, Type type)
    {
        Notification<T> temp = null;
        for (int i = 0; i < noitfications.Count; i++)
        {
            INotification noti = noitfications[i];
            if (!noti.isUsing && noti is INotification<T>)
            {
                temp = (Notification<T>)noti;
                temp.isUsing = true;
                temp.ObserverName = observerName;
                temp.Type = type;
                temp.Body = body;

                return temp;
            }
        }
        temp = new Notification<T>(observerName, body, type);
        noitfications.Add(temp);
        //没有合适的
        return temp;
    }
}
