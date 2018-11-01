using System.Collections.Generic;
using UnityEngine;
namespace PureMVC
{
    public abstract class Mediator : IMediator
    {
        protected string[] acceptors;
        public Mediator(params string[] acceptors)
        {
            this.acceptors = acceptors;
        }
        public virtual IList<string> Acceptors { get { return acceptors; } }
        public abstract void HandleNotification(string observerName);
    }

    public abstract class Mediator<T>: IMediator<T>
    {
        protected string[] acceptors;
        public Mediator(params string[] acceptors)
        {
            this.acceptors = acceptors;
        }
        public virtual IList<string> Acceptors { get { return acceptors; } }
        public abstract void HandleNotification(string observerName, T notification);

    }
}