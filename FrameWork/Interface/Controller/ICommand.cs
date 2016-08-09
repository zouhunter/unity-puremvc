public interface ICommand
{
    void Execute(INotification notify);
}
