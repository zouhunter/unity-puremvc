using System;

namespace PureMVC
{
    public class Observer<T> : IObserver<T>
    {
        public Observer(Action<INotification<T>> notifyMethod, object notifyContext)
        {
            NotifyMethod = notifyMethod;
            NotifyContext = notifyContext;
        }

        public virtual void NotifyObserver(INotification<T> Notification)
        {
            NotifyMethod.Invoke(Notification);
        }
        public virtual bool CompareNotifyContext(object obj)
        {
            return NotifyContext.Equals(obj);
        }

        public Action<INotification<T>> NotifyMethod { get; set; }

        public object NotifyContext { get; set; }

    }
    public class Observer : IObserver
    {
        public Observer(Action<INotification> notifyMethod, object notifyContext)
        {
            NotifyMethod = notifyMethod;
            NotifyContext = notifyContext;
        }
        public virtual void NotifyObserver(INotification Notification)
        {
            NotifyMethod.Invoke(Notification);
        }

        public virtual bool CompareNotifyContext(object obj)
        {
            return NotifyContext.Equals(obj);
        }

        public Action<INotification> NotifyMethod { get; set; }

        public object NotifyContext { get; set; }

    }
}