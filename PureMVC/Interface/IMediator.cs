
using System.Collections.Generic;

namespace PureMVC
{
    public interface IMediator : IAcceptors
    {
        void HandleNotification(int observeKey);
    }

    public interface IMediator<T> : IAcceptors
    {
        void HandleNotification(int observeKey, T notify);
    }
}