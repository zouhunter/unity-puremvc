using UnityEngine;
using System.Collections.Generic;
namespace UnityEngine
{

    public interface IMediator<T> : global::IAcceptor
    {
        void HandleNotification(T notify);
    }
}