using UnityEngine;
using System.Collections;
using System;
using PureMVC.Interfaces;
using PureMVC.Core;
namespace PureMVC
{
    public class Facade : Notifyer, IFacade
    {
        protected IModel m_model;
        protected IView m_view;
        protected IController m_controller;

        public static volatile IFacade Instance = new Facade();
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
        public T RetrieveProxy<T>(string name) where T : IProxy
        {
            return m_model.RetrieveProxy<T>(name);
        }

        public IProxy RemoveProxy(string name)
        {
            return m_model.RemoveProxy(name);
        }

        public void RegisterMediator(IMediatorIner mediator)
        {
            m_view.RegisterMediator(mediator);
        }
        public IMediatorIner RetrieveMediator(string name)
        {
            return m_view.RetrieveMediator<IMediatorIner>(name);
        }

        public T RetrieveMediator<T>(string name) where T : IMediatorIner
        {
            return m_view.RetrieveMediator<T>(name);
        }

        public void RemoveMediator(string name)
        {
            m_view.RemoveMediator(name);
        }

        public void RegisterCommand<T>(string observerName) where T : ICommand, new()
        {
            T cmd = new T();
            m_controller.RegisterCommand(observerName, cmd);
        }

        public ICommand RemoveCommand(string observerName)
        {
            return m_controller.RemoveCommand(observerName);
        }

        public bool HasCommand(string notificationName)
        {
            return m_controller.HasCommand(notificationName);
        }

        public void NotifyObservers<T>(INotification<T> notification)
        {
            m_view.NotifyObservers<T>(notification);
        }
        #endregion
    }
}