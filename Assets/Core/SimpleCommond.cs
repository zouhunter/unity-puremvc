using UnityEngine;

namespace UnityEngine
{

    public abstract class Command : ICommand
    {
        public abstract void Execute(INotification notification);
    }
    public abstract class Command<T> : ICommand<T>
    {
        public abstract void Execute(INotification<T> notification);
    }
}