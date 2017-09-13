namespace UnityEngine
{
    public interface ICommand 
    {
        void Execute();
    }
    public interface ICommand<T> : ICommand
    {
        void Execute(T notify);
    }
}
