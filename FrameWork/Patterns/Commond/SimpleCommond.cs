
public abstract class SimpleCommand : Notifyer, ICommand
{
    public abstract void Execute(INotification notification);
}
