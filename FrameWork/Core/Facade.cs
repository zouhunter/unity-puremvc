using UnityEngine;
using System.Collections;
using System;

public class Facade :Notifyer, IFacade
{
    protected IModel m_model;
    protected IView m_view;
    protected IController m_controller;

    public static IFacade Instance = new Facade();
    protected readonly object m_syncRoot = new object();
    public Facade()
    {
        InitializeFacade();
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
        return m_model.RetrieveProxy<IProxy>(name);
    }
    public T RetrieveProxy<T>(string name) where T : IProxy
    {
        return m_model.RetrieveProxy<T>(name);
    }

    public IProxy RemoveProxy(string name)
    {
        return m_model.RemoveProxy(name);
    }

    public void RegisterMediator(IMediator mediator)
    {
        m_view.RegisterMediator(mediator);
    }
    public IMediator RetrieveMediator(string name)
    {
        return m_view.RetrieveMediator<IMediator>(name);
    }

    public T RetrieveMediator<T>(string name) where T : IMediator
    {
        return m_view.RetrieveMediator<T>(name);
    }

    public void RemoveMediator(string name)
    {
        m_view.RemoveMediator(name);
    }

    public void RegisterCommand(string observerName, Type cmd)
    {
        m_controller.RegisterCommand(observerName, cmd);
    }

    public Type RemoveCommand(string observerName)
    {
        return m_controller.RemoveCommand(observerName);
    }
    #endregion
}
