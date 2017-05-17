using UnityEngine;
using System;

namespace UnityEngine
{
    public class Observer<T> : IObserver<T>
    {
        public Observer(UnityEngine.Events.UnityAction<INotification<T>> notifyMethod, object notifyContext)
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

        public UnityEngine.Events.UnityAction<INotification<T>> NotifyMethod { get; set; }

        public object NotifyContext { get; set; }

    }
    public class Observer : IObserver
    {
        public Observer(UnityEngine.Events.UnityAction<INotification> notifyMethod, object notifyContext)
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

        public UnityEngine.Events.UnityAction<INotification> NotifyMethod { get; set; }

        public object NotifyContext { get; set; }

    }
}