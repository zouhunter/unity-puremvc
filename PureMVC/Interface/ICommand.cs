
namespace PureMVC
{
    public interface ICommandpublic
    {
    }

    public interface ICommand : ICommandpublic
    {
        void Execute();
    }
    public interface ICommand<T> : ICommandpublic
    {
        void Execute(T data);
    }
}