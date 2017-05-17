using UnityEngine;
using System.Collections.Generic;
namespace UnityEngine
{

    public interface IMediator
    {
        IList<string> ListNotificationInterests();
    }
  
    public interface IMediator<T> : IMediator
    {
        void HandleNotification(T notify);
    }
}