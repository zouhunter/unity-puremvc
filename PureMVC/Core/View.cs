using System;
using System.Collections.Generic;

namespace PureMVC
{
    public class View : IView
    {
        protected IList<IAcceptors> m_mediatorMap;
        protected IDictionary<int, List<IObserverBase>> m_observerMap;
        protected static volatile IView m_instance;
        internal View()
        {
            m_mediatorMap = new List<IAcceptors>();
            m_observerMap = new Dictionary<int, List<IObserverBase>>();
        }

        /// <summary>
        /// 注册成为观察者
        /// </summary>
        /// <param name="obName"></param>
        /// <param name="observer"></param>
        public void RegisterObserver(int eventName, IObserverBase observer)
        {
            if (m_observerMap.ContainsKey(eventName))
            {
                if (!m_observerMap[eventName].Contains(observer))
                {
                    m_observerMap[eventName].Add(observer);
                }
            }
            else
            {
                m_observerMap.Add(eventName, new List<IObserverBase>() { observer });
            }
        }
        /// <summary>
        /// 通知所有观察者
        /// </summary>
        /// <param name="notify"></param>
        public void NotifyObservers<T>(INotification<T> noti)
        {
            IList<IObserverBase> observers = null;

            if (m_observerMap.ContainsKey(noti.ObserverName))
            {
                IList<IObserverBase> observers_ref = m_observerMap[noti.ObserverName];
                observers = new List<IObserverBase>(observers_ref);
            }

            if (observers != null)
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    IObserverBase observer = observers[i];
                    if (observer is IObserver<T>)
                    {
                        (observer as IObserver<T>).NotifyObserver(noti);
                    }
                    else if (observer is IObserver<object>)
                    {
                        Notification<object> notify = Notification<object>.Allocate(noti.ObserverName, noti.Body, noti.Type);
                        (observer as IObserver<object>).NotifyObserver(notify);
                        notify.Release();
                    }
                    else if (observer is IObserver)
                    {
                        (observer as IObserver).NotifyObserver(noti);
                    }
                }
            }
        }
        /// <summary>
        /// 通知所有观察者
        /// </summary>
        /// <param name="notify"></param>
        public void NotifyObservers(INotification noti)
        {
            IList<IObserverBase> observers = null;

            if (m_observerMap.ContainsKey(noti.ObserverName))
            {
                IList<IObserverBase> observers_ref = m_observerMap[noti.ObserverName];
                observers = new List<IObserverBase>(observers_ref);
            }

            if (observers != null)
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    IObserverBase observer = observers[i];
                    if (observer is IObserver)
                    {
                        (observer as IObserver).NotifyObserver(noti);
                    }
                }
            }
        }
        /// <summary>
        /// 将指定的观察者移除
        /// </summary>
        /// <param name="name"></param>
        public void RemoveObserver(int eventName, object notifyContext)
        {
            // the observer list for the notification under inspection
            if (m_observerMap.ContainsKey(eventName))
            {
                IList<IObserverBase> observers = m_observerMap[eventName];

                for (int i = 0; i < observers.Count; i++)
                {
                    if (observers[i].CompareNotifyContext(notifyContext))
                    {
                        observers.RemoveAt(i);
                        break;
                    }
                }

                if (observers.Count == 0)
                {
                    m_observerMap.Remove(eventName);
                }
            }
        }
        /// <summary>
        /// 将所有的观察者移除
        /// </summary>
        /// <param name="name"></param>
        public void RemoveObservers(int eventName)
        {
            if (m_observerMap.ContainsKey(eventName))
            {
                m_observerMap.Remove(eventName);
            }
        }
        /// <summary>
        /// 注册mediator
        /// </summary>
        /// <param name="notify"></param>
        public void RegisterMediator<T>(IMediator<T> mediator)
        {
            if (m_mediatorMap.Contains(mediator)) return;

            // Register the Mediator for retrieval by name
            m_mediatorMap.Add(mediator);

            // Create Observer

            if (mediator.Acceptors != null)
            {
                foreach (var acceptor in mediator.Acceptors)
                {
                    IObserver<T> observer = new Observer<T>((x) => (mediator as IMediator<T>).HandleNotification(acceptor, x.Body), mediator);
                    RegisterObserver(acceptor, observer);
                }
            }
        }
        public void RegisterMediator(IMediator mediator)
        {
            if (m_mediatorMap.Contains(mediator)) return;

            // Register the Mediator for retrieval by name
            m_mediatorMap.Add(mediator);

            // Create Observer

            if (mediator.Acceptors != null)
            {
                foreach (var acceptor in mediator.Acceptors)
                {
                    IObserver observer = new Observer((x) => (mediator as IMediator).HandleNotification(acceptor), mediator);
                    RegisterObserver(acceptor, observer);
                }
            }
        }

        public void RemoveMediator(IMediator mediator)
        {
            if (!m_mediatorMap.Contains(mediator)) return;

            foreach (var Acceptor in mediator.Acceptors)
            {
                RemoveObserver(Acceptor, mediator);
            }

            m_mediatorMap.Remove(mediator);
        }
        /// <summary>
        /// 是否含有观察者
        /// </summary>
        /// <param name="observerName"></param>
        /// <returns></returns>
        public bool HasObserver(int observerName)
        {
            return m_observerMap.ContainsKey(observerName);
        }

        public void RemoveMediator<T>(IMediator<T> mediator)
        {
            if (!m_mediatorMap.Contains(mediator)) return;

            foreach (var Acceptor in mediator.Acceptors)
            {
                RemoveObserver(Acceptor, mediator);
            }

            m_mediatorMap.Remove(mediator);
        }
    }
}