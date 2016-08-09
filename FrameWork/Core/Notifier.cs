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
	private void NotifyObservers<T>(INotification<T> notification)
    {
        m_view.NotifyObservers<T>(notification);
    }
	public void SendNotification(ObserverName observeName)
	{
		NotifyObservers(new Notification<object>(observeName));
	}
	public void SendNotification<T>(ObserverName observeName)
    {
        NotifyObservers(new Notification<T>(observeName));
    }
	public void SendNotification<T>(ObserverName observeName, T body)
    {
        NotifyObservers(new Notification<T>(observeName, body));
    }
	public void SendNotification<T>(ObserverName observeName, T body, Type type)
    {
        NotifyObservers(new Notification<T>(observeName, body, type));
    }

}
