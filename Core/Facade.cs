using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using System.Collections.Generic;
using ZLab.Model;

namespace UnityEngine
{

    public static class Facade
    {
        static IModel m_model;
        static IView m_view;
        static IController m_controller;
        static Facade()
        {
            InitializeFacade();
        }
        private static void InitializeFacade()
        {
            InitializeModel();
            InitializeController();
            InitializeView();
        }
        private static void InitializeController()
        {
            if (m_controller != null) return;
            m_controller = Controller.Instance;
        }
        private static void InitializeModel()
        {
            if (m_model != null) return;
            m_model = Model.Instance;
        }
        private static void InitializeView()
        {
            if (m_view != null) return;
            m_view = View.Instance;
        }

        #region 访问三大层的
        public static void RegisterProxy<T>(IProxy<T> prox)
        {
            m_model.RegisterProxy(prox);
        }

        public static void CansaleRetrieve(string name)
        {
            m_model.CansaleRetrieve(name);
        }

        public static void RetrieveProxy<T>(string name, UnityAction<IProxy<T>> onRetieved)
        {
            m_model.RetrieveProxy<T>(name, onRetieved);
        }
        public static void RetrieveData<T>(string name, UnityAction<T> onRetieved)
        {
            m_model.RetrieveData<T>(name, onRetieved);
        }
        public static bool HaveProxy(string name)
        {
           return  m_model.HasProxy(name);
        }

        public static IProxy<T> RemoveProxy<T>(string name)
        {
            return m_model.RemoveProxy<T>(name);
        }


        public static void RegisterMediator<T>(IMediator<T> mediator)
        {
            m_view.RegisterMediator(mediator);
        }

        public static void RemoveMediator<T>(IMediator<T> mediator)
        {
            m_view.RemoveMediator(mediator);
        }

        public static void RegisterCommand<T,P>(string observeName) where T : ICommand<P>, new()
        {
            m_controller.RegisterCommand<T,P>(observeName);
        }
        public static void RegisterCommand<T>(string observeName) where T : ICommand, new()
        {
            m_controller.RegisterCommand<T>(observeName);
        }
        public static void RemoveCommand(string observerName)
        {
            m_controller.RemoveCommand(observerName);
        }

        /// <summary>
        /// 通知观察者
        /// </summary>
        /// <param name="notification"></param>
        private static void NotifyObservers<T>(INotification<T> notification)
        {
            if (m_view.HasObserver(notification.ObserverName))
            {
                m_view.NotifyObservers<T>(notification);
            }
        }

        public static void SendNotification(string observeName)
        {
            SendNotification<object>(observeName, null, null);
        }
        public static void SendNotification<T>(string observeName, T body)
        {
            SendNotification<T>(observeName, body, null);
        }
        public static void SendNotification<T>(string observeName, T body, Type type)
        {
            Notification<T> notify = Notification<T>.Allocate(observeName, body, type);
            NotifyObservers(notify);
            notify.Release();
        }

        #endregion

    }

}
