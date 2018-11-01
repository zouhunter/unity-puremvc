using System.Collections.Generic;
using UnityEngine;
namespace PureMVC
{
    public abstract class Mediator : IMediator
    {
        public virtual string Acceptor { get { return null; } }

        public virtual IList<string> Acceptors { get { return null; } }

        public abstract void HandleNotification(string observerName);
    }

    public abstract class Mediator<T>: IMediator<T>
    {
        public virtual string Acceptor { get { return null; } }

        public virtual IList<string> Acceptors { get { return null; } }

        public abstract void HandleNotification(string observerName, T notification);

    }
}