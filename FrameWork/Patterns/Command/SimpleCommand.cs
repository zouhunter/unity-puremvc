
public abstract class SimpleCommand : Notifier, ICommand
{
    public abstract void Execute(INotification notification);
}
