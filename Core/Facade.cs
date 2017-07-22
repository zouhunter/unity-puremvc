using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

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
        public static void RegisterProxy(IAcceptor prox)
        {
            m_model.RegisterProxy(prox);
        }

        public static void CansaleRetrieve(string name)
        {
            m_model.CansaleRetrieve(name);
        }

        public static void RetrieveProxy<T>(string name, UnityAction<T> onRetieved)
        {
            m_model.RetrieveProxy<T>(name, onRetieved);
        }

        public static void RemoveMediator<T>(IMediator<T> mediator)
        {
            m_view.RemoveMediator(mediator);
        }


        public static IAcceptor RemoveProxy(string name)
        {
            return m_model.RemoveProxy(name);
        }

        public static void RegisterMediator<T>(IMediator<T> mediator)
        {
            m_view.RegisterMediator(mediator);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">command类型</typeparam>
        /// <typeparam name="P">参数类型</typeparam>
        public static void RegisterCommand<T,P>()
        {
            m_controller.RegisterCommand<P>(typeof(T));
        }
        public static void RegisterCommand<T>()
        {
            m_controller.RegisterCommand(typeof(T));
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
