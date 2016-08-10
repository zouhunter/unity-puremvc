using System;

namespace PureMVC.Interfaces
{
    public interface INotification
    {
        string ObserverName { get; set; }
        string ToString { get; }
        Type Type { get; set; }
        bool isUsing { get; set; }
    }

    public interface INotification<T> : INotification
    {
        T Body { get; set; }
    }
}