using System;

public class Notifyer : INotifier {
    private IView m_view;
    public Notifyer()
    {
        m_view = View.Instance;
    }
    /// <summary>
    /// 通知观察者
    /// </summary>
    /// <param name="notification"></param>
    public virtual void NotifyObservers<T>(INotification<T> notification)
    {
        m_view.NotifyObservers<T>(notification);
    }
    public virtual void SendNotification(string observeName)
    {
        NotifyObservers(new Notification<object>(observeName));
    }
    public virtual void SendNotification<T>(string observeName, T body)
    {
        NotifyObservers(new Notification<T>(observeName, body));
    }
    public virtual void SendNotification<T>(string observeName, T body, Type type)
    {
        NotifyObservers(new Notification<T>(observeName, body, type));
    }

}
