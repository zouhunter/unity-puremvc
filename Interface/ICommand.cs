namespace UnityEngine
{
    public interface ICommand : global::IAcceptor
    {
        void Execute();
    }
    public interface ICommand<T> : global::IAcceptor
    {
        void Execute(T notify);
    }
}
