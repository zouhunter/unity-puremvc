using System;


namespace PureMVC
{
    public interface INotifier
    {
        void SendNotification(int notificationName);
        void SendNotification<T>(int notificationName, T body);
    }
}