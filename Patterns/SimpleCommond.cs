using PureMVC.Interfaces;

namespace PureMVC
{
    public abstract class SimpleCommand : Notifyer, ICommand
    {
        public abstract void Execute(INotification<object> notification);
    }
    public abstract class ValueCommand<T> : Notifyer, ICommand<T>
    {
        public abstract void Execute(INotification<T> notification);
    }
}
