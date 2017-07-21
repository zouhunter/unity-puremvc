using UnityEngine;

namespace UnityEngine
{
    public abstract class Command : ICommand
    {
        public abstract void Execute();
    }
    public abstract class Command<T> : ICommand<T>
    {
        public abstract void Execute(T notification);
    }
}