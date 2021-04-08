using System.Collections.Generic;

namespace PureMVC
{
    public abstract class Mediator : IMediator
    {
        protected int[] acceptors;
        public Mediator(params int[] acceptors)
        {
            this.acceptors = acceptors;
        }
        public virtual IList<int> Acceptors { get { return acceptors; } }
        public abstract void HandleNotification(int observeKey);
    }

    public abstract class Mediator<T>: IMediator<T>
    {
        protected int[] acceptors;
        public Mediator(params int[] acceptors)
        {
            this.acceptors = acceptors;
        }
        public virtual IList<int> Acceptors { get { return acceptors; } }
        public abstract void HandleNotification(int observeKey, T notification);

    }
}