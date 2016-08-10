namespace PureMVC.Interfaces
{
    public interface ICommand { }
    public interface ICommand<T> : ICommand
    {
        void Execute(INotification<T> notify);
    }
}
