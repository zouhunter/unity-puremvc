using System;

namespace PureMVC
{
    public interface INotification
    {
        int ObserverName { get; set; }
        string ToString { get; }
        bool Destroy { get; set; }
        bool IsUsing { get; set; }
    }
    public interface INotification<T> : INotification
    {
        T Body { get; set; }
    }
}