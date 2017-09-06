using UnityEngine;

namespace UnityEngine
{
    public abstract class Command : ICommand
    {
        public virtual string Acceptor { get { return this.GetType().ToString(); } }
        public abstract void Execute();
    }
    public abstract class Command<T> : ICommand<T>
    {
        public virtual string Acceptor { get { return this.GetType().ToString(); } }
        public virtual void Execute() { }
        public abstract void Execute(T notification);
    }
}