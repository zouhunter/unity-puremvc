using UnityEngine;
using System.Collections.Generic;
namespace UnityEngine
{
    public interface IMediator: IAcceptors
    {
        void HandleNotification(string observerName);
    }

    public interface IMediator<T> : IAcceptors
    {
        void HandleNotification(string observerName,T notify);
    }
}