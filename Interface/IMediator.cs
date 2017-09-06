using UnityEngine;
using System.Collections.Generic;
namespace UnityEngine
{

    public interface IMediator<T> : IAcceptors
    {
        void HandleNotification(T notify);
    }
}