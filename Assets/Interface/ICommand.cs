namespace UnityEngine
{
    public interface ICommandBase { }
    public interface ICommand : ICommandBase
    {
        void Execute();
    }
    public interface ICommand<T> : ICommandBase
    {
        void Execute(T notify);
    }
}
