using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class View : IView
{
    protected IDictionary<string, IMediator> m_mediatorMap;
    protected IDictionary<NotiConst, List<IObserver>> m_observerMap;

    protected View()
    {
        m_mediatorMap = new Dictionary<string, IMediator>();
        m_observerMap = new Dictionary<NotiConst, List<IObserver>>();
        InitializeView();
    }
    protected static volatile IView m_instance;
    protected readonly object m_syncRoot = new object();
    protected static readonly object m_staticSyncRoot = new object();
    public static IView Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance == null)
                    {
                        m_instance = new View();
                    }
                }
            }

            return m_instance;
        }
    }
    protected virtual void InitializeView()
    {

    }
    /// <summary>
    /// 注册成为观察者
    /// </summary>
    /// <param name="obName"></param>
    /// <param name="observer"></param>
    public void RegisterObserver(NotiConst noti, IObserver observer)
    {
        lock (m_syncRoot)
        {
            if (m_observerMap.ContainsKey(noti))
            {
                if (!m_observerMap[noti].Contains(observer))
                {
                    m_observerMap[noti].Add(observer);
                }
            }
            else
            {
                m_observerMap.Add(noti, new List<IObserver>() { observer });
            }
        }
    }
    /// <summary>
    /// 通知所有观察者
    /// </summary>
    /// <param name="notify"></param>
    public virtual void NotifyObservers(INotification noti)
    {
        IList<IObserver> observers = null;

        lock (m_syncRoot)
        {
            if (m_observerMap.ContainsKey(noti.ObserverName))
            {
                IList<IObserver> observers_ref = m_observerMap[noti.ObserverName];
                observers = new List<IObserver>(observers_ref);
            }
        }

        if (observers != null)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                IObserver observer = observers[i];
                observer.NotifyObserver(noti);
            }
        }
    }
    /// <summary>
    /// 将指定的观察者移除
    /// </summary>
    /// <param name="name"></param>
    public virtual void RemoveObserver(NotiConst notificationName, object notifyContext)
    {
        lock (m_syncRoot)
        {
            // the observer list for the notification under inspection
            if (m_observerMap.ContainsKey(notificationName))
            {
                IList<IObserver> observers = m_observerMap[notificationName];

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
                    m_observerMap.Remove(notificationName);
                }
            }
        }
    }
    /// <summary>
    /// 将所有的观察者移除
    /// </summary>
    /// <param name="name"></param>
    public void RemoveObservers(NotiConst obName)
    {
        ////Debug.Log("移除 ： " + obName);
        if (m_observerMap.ContainsKey(obName))
        {
            m_observerMap.Remove(obName);
        }
    }
    /// <summary>
    /// 注册中介
    /// </summary>
    /// <param name="notify"></param>
    public void RegisterMediator(IMediator mediator)
    {
        lock (m_syncRoot)
        {
            if (m_mediatorMap.ContainsKey(mediator.MediatorName)) return;

            // Register the Mediator for retrieval by name
            m_mediatorMap[mediator.MediatorName] = mediator;

            // Get Notification interests, if any.
            IList<NotiConst> interests = mediator.ListNotificationInterests();

            // Register Mediator as an observer for each of its notification interests
            if (interests.Count > 0)
            {
                // Create Observer
                IObserver observer = new Observer("HandleNotification",mediator);

                // Register Mediator as Observer for its list of Notification interests
                for (int i = 0; i < interests.Count; i++)
                {
                    RegisterObserver(interests[i], observer);
                }
            }
        }
        // alert the mediator that it has been registered
        mediator.OnRegister();
    }
    /// <summary>
    /// 获取中介
    /// </summary>
    /// <param name="notify"></param>
    /// <returns></returns>
    public IMediator RetrieveMediator(string Name)
    {
        lock (m_syncRoot)
        {
            if (!m_mediatorMap.ContainsKey(Name)) return null;
            return m_mediatorMap[Name];
        }
    }
    public T RetrieveMediator<T>(string Name) where T : IMediator
    {
        lock (m_syncRoot)
        {
            if (!m_mediatorMap.ContainsKey(Name)) return default(T);
            IMediator manager = null;
            m_mediatorMap.TryGetValue(Name, out manager);
            return (T)manager;
        }
    }
    /// <summary>
    /// 移除中介
    /// </summary>
    /// <param name="notify"></param>
    public IMediator RemoveMediator(string mediatorName)
    {
        IMediator mediator = null;

        lock (m_syncRoot)
        {
            if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
            mediator = (IMediator)m_mediatorMap[mediatorName];

            IList<NotiConst> interests = mediator.ListNotificationInterests();

            for (int i = 0; i < interests.Count; i++)
            {
                RemoveObserver(interests[i], mediator);
            }

            m_mediatorMap.Remove(mediatorName);
        }

        if (mediator != null) mediator.OnRemove();
        return mediator;
    }
    /// <summary>
    /// 是否有mdeiator
    /// </summary>
    /// <param name="mediatorName"></param>
    /// <returns></returns>
    public virtual bool HasMediator(string mediatorName)
    {
        lock (m_syncRoot)
        {
            return m_mediatorMap.ContainsKey(mediatorName);
        }
    }

}