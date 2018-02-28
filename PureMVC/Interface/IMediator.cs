
using System.Collections.Generic;

namespace PureMVC
{
    public interface IMediator : IAcceptors
    {
        void HandleNotification(string observerName);
    }

    public interface IMediator<T> : IAcceptors
    {
        void HandleNotification(string observerName, T notify);
    }
}