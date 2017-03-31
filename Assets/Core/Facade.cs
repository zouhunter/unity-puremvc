using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using PureMVC.Internal;

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

        public void CansaleRetrieve(string name)
        {
            m_model.CansaleRetrieve(name);
        }

        public void RetrieveProxy<T>(string name, UnityAction<T> onRetieved) where T : IProxy
        {
            m_model.RetrieveProxy<T>(name, onRetieved);
        }

        public void RemoveMediator(IMediator name)
        {
            m_view.RemoveMediator(name);
        }


        public IProxy RemoveProxy(string name)
        {
            return m_model.RemoveProxy(name);
        }

        public void RegisterMediator(IMediator mediator)
        {
            m_view.RegisterMediator(mediator);
        }

        public void RegisterCommand<T>(string observerName) where T : ICommand, new()
        {
            m_controller.RegisterCommand(observerName, typeof(T));
        }

        public void RemoveCommand(string observerName)
        {
            m_controller.RemoveCommand(observerName);
        }

        #endregion

    }

}
