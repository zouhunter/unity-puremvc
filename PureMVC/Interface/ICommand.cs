
namespace PureMVC
{
    public interface ICommandInternal
    {
    }

    public interface ICommand : ICommandInternal
    {
        void Execute();
    }
    public interface ICommand<T> : ICommandInternal
    {
        void Execute(T data);
    }
}