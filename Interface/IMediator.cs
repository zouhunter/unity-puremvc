using UnityEngine;
using System.Collections.Generic;
namespace UnityEngine
{

    public interface IMediator<T> : IAcceptor
    {
        void HandleNotification(T notify);
    }
}