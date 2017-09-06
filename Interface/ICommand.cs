namespace UnityEngine
{
    public interface ICommand : IAcceptor
    {
        void Execute();
    }
    public interface ICommand<T> : ICommand
    {
        void Execute(T notify);
    }
}
