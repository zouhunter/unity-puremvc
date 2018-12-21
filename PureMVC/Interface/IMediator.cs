
using System.Collections.Generic;

namespace PureMVC
{
    public interface IMediator : IAcceptors
    {
        void HandleNotification(int observerName);
    }

    public interface IMediator<T> : IAcceptors
    {
        void HandleNotification(int observerName, T notify);
    }
}