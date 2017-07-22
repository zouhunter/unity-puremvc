namespace UnityEngine
{
    public interface ICommand : IAcceptor
    {
        void Execute();
    }
    public interface ICommand<T> : IAcceptor
    {
        void Execute(T notify);
    }
}
