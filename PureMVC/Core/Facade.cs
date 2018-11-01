using System;
namespace PureMVC
{
    public class Facade
    {
        protected IModel m_model;
        protected IView m_view;
        protected IController m_controller;

        public Facade()
        {
            InitializeFacade();
        }

        protected virtual void InitializeFacade()
        {
            m_view = InitializeView();
            InitializeController(m_view);
            InitializeModel();
        }

        protected virtual IController InitializeController(IView view)
        {
            return new Controller(this,view);
        }
        protected virtual IModel InitializeModel()
        {
            return new Model(this);
        }
        protected virtual IView InitializeView()
        {
            return new View(this);
        }

        #region 访问三大层的
        public void RegisterProxy<T>(IProxy<T> prox)
        {
            m_model.RegisterProxy(prox);
        }
        public void CansaleRetrieve(string name)
        {
            m_model.CansaleRetrieve(name);
        }

        public P RetrieveProxy<P, T>(string name) where P : IProxy<T>
        {
            return m_model.RetrieveProxy<P, T>(name);
        }
        public void RetrieveProxy<P, T>(string name, Action<P> onRetieved) where P : IProxy<T>
        {
            m_model.RetrieveProxy<P, T>(name, onRetieved);
        }
        public void RetrieveProxy<T>(string name, Action<IProxy<T>> onRetieved)
        {
            m_model.RetrieveProxy<T>(name, onRetieved);
        }
        public IProxy<T> RetrieveProxy<T>(string name)
        {
            return m_model.RetrieveProxy<T>(name);
        }
        public void RetrieveData<T>(string name, Action<T> onRetieved)
        {
            m_model.RetrieveData<T>(name, onRetieved);
        }
        public T RetrieveData<T>(string name)
        {
            return m_model.RetrieveData<T>(name);
        }
        public bool HaveProxy(string name)
        {
            return m_model.HasProxy(name);
        }

        public void RemoveProxy(string name)
        {
            m_model.RemoveProxy(name);
        }


        public void RegisterMediator<T>(IMediator<T> mediator)
        {
            m_view.RegisterMediator(mediator);
        }
        public void RegisterMediator(IMediator mediator)
        {
            m_view.RegisterMediator(mediator);
        }
        public void RemoveMediator(IMediator mediator)
        {
            m_view.RemoveMediator(mediator);
        }
        public void RemoveMediator<T>(IMediator<T> mediator)
        {
            m_view.RemoveMediator(mediator);
        }

        public void RegisterCommand<T, P>(string observeName) where T : ICommand<P>, new()
        {
            m_controller.RegisterCommand<T, P>(observeName);
        }

        public void RegisterCommand<T>(string observeName) where T : ICommand, new()
        {
            m_controller.RegisterCommand<T>(observeName);
        }
        public void RemoveCommand(string observerName)
        {
            m_controller.RemoveCommand(observerName);
        }

        /// <summary>
        /// 通知观察者
        /// </summary>
        /// <param name="notification"></param>
        protected void NotifyObservers<T>(INotification<T> notification)
        {
            if (m_view.HasObserver(notification.ObserverName))
            {
                m_view.NotifyObservers<T>(notification);
            }
        }

        public void SendNotification(string observeName)
        {
            SendNotification<object>(observeName, null, null);
        }
        public void SendNotification<T>(string observeName, T body)
        {
            SendNotification<T>(observeName, body, null);
        }
        public void SendNotification<T>(string observeName, T body, Type type)
        {
            Notification<T> notify = Notification<T>.Allocate(observeName, body, type);
            NotifyObservers(notify);
            notify.Release();
        }

        #endregion

    }

}