using System;
public interface INotifier
{
	void SendNotification(ObserverName notificationName);
	void SendNotification<T>(ObserverName notificationName, T body);
	void SendNotification<T>(ObserverName notificationName, T body, Type type);
}
