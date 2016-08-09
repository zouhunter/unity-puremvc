using UnityEngine;
using System.Collections;
using System;

public class Facade : IFacade, INotifier
{
    protected IModel m_model;
    protected IView m_view;
    protected IController m_controller;
    protected static volatile IFacade m_instance;
    protected static readonly object m_staticSyncRoot = new object();

    static Facade()
    {
    }
    protected Facade()
    {
        InitializeFacade();
    }
    public static IFacade Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance == null)
                        m_instance = new Facade();
                }
            }

            return m_instance;
        }
    }

    protected virtual void InitializeFacade()
    {
        InitializeModel();
        InitializeController();
        InitializeView();
    }
    protected virtual void InitializeController()
    {
        if (m_controller != null) return;
        m_controller = Controller.Instance;
    }
    protected virtual void InitializeModel()
    {
        if (m_model != null) return;
        m_model = Model.Instance;
    }
    protected virtual void InitializeView()
    {
        if (m_view != null) return;
        m_view = View.Instance;
    }

    #region 访问三大层的
    public void RegisterProxy(IProxy prox)
    {
        m_model.RegisterProxy(prox);
    }

    public IProxy RetrieveProxy(string name)
    {
       return m_model.RetrieveProxy(name);
    }

    public void RemoveProxy(string name)
    {
        m_model.RemoveProxy(name);
    }

    public void RegisterMediator(IMediator mediator)
    {
        m_view.RegisterMediator(mediator);
    }
    public IMediator RetrieveMediator(string name)
    {
        return m_view.RetrieveMediator(name);
    }

    public T RetrieveMediator<T>(string name) where T : IMediator
    {
        return m_view.RetrieveMediator<T>(name);
    }

    public void RemoveMediator(string name)
    {
        m_view.RemoveMediator(name);
    }

    public void RegisterCommand(NotiConst name,Type cmd)
    {
        m_controller.RegisterCommand(name,cmd);
    }

    public void RemoveCommand(NotiConst noti)
    {
        m_controller.RemoveCommand(noti);
    }

    public void RemoveMultiCommand(NotiConst[] commandName) { 
        int count = commandName.Length;
        for (int i = 0; i < count; i++)
        {
            m_controller.RemoveCommand(commandName[i]);
        }
    }
    #endregion

    /// <summary>
    /// 通知观察者
    /// </summary>
    /// <param name="notification"></param>
    public virtual void NotifyObservers(INotification notification)
    {
        m_view.NotifyObservers(notification);
    }
    public virtual void SendNotification(NotiConst notificationName)
    {
        NotifyObservers(new Notification(notificationName));
    }
    public virtual void SendNotification(NotiConst notificationName, object body)
    {
        NotifyObservers(new Notification(notificationName, body));
    }
    public virtual void SendNotification(NotiConst notificationName, object body, Type type)
    {
        NotifyObservers(new Notification(notificationName, body, type));
    }

}
