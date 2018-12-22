using System;
namespace PureMVC
{
    public class Facade
    {
        protected IModel m_model;
        protected IView m_view;
        protected IController m_controller;
        
        protected Action<int> notifyNotHandle { get; set; }
        public Facade()
        {
            InitializeFacade();
        }

        protected virtual void InitializeFacade()
        {
            m_view = InitializeView();
            m_controller = InitializeController(m_view);
            m_model = InitializeModel();
        }

        protected virtual IController InitializeController(IView view)
        {
            return new Controller(view);
        }
        protected virtual IModel InitializeModel()
        {
            return new Model();
        }
        protected virtual IView InitializeView()
        {
            return new View();
        }

        #region 访问三大层的
        public void RegisterProxy<T>(IProxy<T> prox)
        {
            m_model.RegisterProxy(prox);
        }
        public void RegisterProxy<T>(int proxyName, T data)
        {
            if(m_model.HasProxy(proxyName))
            {
                m_model.RetrieveProxy<T>(proxyName).Data = data;
            }
            else
            {
                m_model.RegisterProxy(new Proxy<T>(proxyName,data));
            }
        }
        public void CansaleRetrieve(int name)
        {
            m_model.CansaleRetrieve(name);
        }

        public P RetrieveProxy<P, T>(int name) where P : IProxy<T>
        {
            return m_model.RetrieveProxy<P, T>(name);
        }
        public void RetrieveProxy<P, T>(int name, Action<P> onRetieved) where P : IProxy<T>
        {
            m_model.RetrieveProxy<P, T>(name, onRetieved);
        }
        public void RetrieveProxy<T>(int name, Action<IProxy<T>> onRetieved)
        {
            m_model.RetrieveProxy<T>(name, onRetieved);
        }
        public IProxy<T> RetrieveProxy<T>(int name)
        {
            return m_model.RetrieveProxy<T>(name);
        }
        public void RetrieveData<T>(int name, Action<T> onRetieved)
        {
            m_model.RetrieveData<T>(name, onRetieved);
        }
        public T RetrieveData<T>(int name)
        {
            return m_model.RetrieveData<T>(name);
        }
        public bool HaveProxy(int name)
        {
            return m_model.HasProxy(name);
        }
        public void RemoveProxy(int name)
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

        public void RegisterCommand<T, P>(int observeName) where T : ICommand<P>, new()
        {
            m_controller.RegisterCommand<T, P>(observeName);
        }

        public void RegisterCommand<T>(int observeName) where T : ICommand, new()
        {
            m_controller.RegisterCommand<T>(observeName);
        }

        public void RemoveCommand(int observerName)
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
            else
            {
                if (notifyNotHandle != null)
                {
                    notifyNotHandle.Invoke(notification.ObserverName);
                }
            }
        }

        public void SendNotification(int observeName)
        {
            SendNotification<object>(observeName, null);
        }
        public void SendNotification<T>(int observeName, T body)
        {
            Notification<T> notify = Notification<T>.Allocate(observeName, body);
            NotifyObservers(notify);
            notify.Release();
        }
        #endregion

    }

}