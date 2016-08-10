using UnityEngine;
using System.Collections.Generic;

namespace PureMVC.Interfaces
{
    public interface IMediatorIner
    {
        string MediatorName { get; }
        string[] ListNotificationInterests();
        void OnRegister();
        void OnRemove();
    }

    public interface IMediator : IMediatorIner
    {
        void HandleNotification(INotification notify);
    }
    public interface IMediator<T> : IMediatorIner
    {
        void HandleNotification(INotification<T> notify);
    }
    public interface IMediator<T,S> : IMediatorIner
    {
        void HandleNotification(INotification<T> notify);
        S Component { get; }
    }
}