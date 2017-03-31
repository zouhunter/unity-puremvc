using System;
namespace PureMVC.Internal
{

    public interface INotifier
    {
        void SendNotification(string notificationName);
        void SendNotification<T>(string notificationName, T body);
        void SendNotification<T>(string notificationName, T body, Type type);
    }
}