using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Controller : IController
    {
        protected IView m_view;
        protected IDictionary<string, Func<ICommand>> m_commandMap;
        protected static volatile IController m_instance;
        protected Controller()
        {
            m_commandMap = new Dictionary<string, Func<ICommand>>();
            InitializeController();
        }
        public static IController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    if (m_instance == null) m_instance = new Controller();
                }

                return m_instance;
            }
        }

        static Controller()
        {

        }
        protected virtual void InitializeController()
        {
            m_view = View.Instance;
        }
        /// <summary>
        /// 注册非泛型命令
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="newType"></param>
        public virtual void RegisterCommand<T>(string observeName) where T : ICommand, new()
        {
            if (typeof(T).BaseType.IsGenericType)
            {
                Debug.Log("请注册" + observeName + "的参数类型" +
                    "\n否则不会调用带参数的方法");
            }

            if (!m_commandMap.ContainsKey(observeName))
            {
                IObserver observer = new Observer((notification) =>
                {
                    Func<ICommand> acceptor;
                    if (m_commandMap.TryGetValue(notification.ObserverName, out acceptor))
                    {
                        (acceptor()).Execute();
                    }
                }, this);
                m_view.RegisterObserver(observeName, observer);
            }
            m_commandMap[observeName] = new Func<ICommand>(()=>new T());
        }
        /// <summary>
        /// 注册泛型命令
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="notificationName"></param>
        /// <param name="newcommandFunc"></param>
        public virtual void RegisterCommand<T, P>(string observeName) where T : ICommand<P>, new()
        {
            if (!m_commandMap.ContainsKey(observeName))
            {
                IObserver<P> observer = new Observer<P>((notification) =>
                {
                   Func<ICommand> acceptor;
                    if (m_commandMap.TryGetValue(notification.ObserverName, out acceptor))
                    {
                        (acceptor() as ICommand<P>).Execute(notification.Body);
                        (acceptor() as ICommand).Execute();
                    }
                }, this);
                m_view.RegisterObserver(observeName, observer);
            }
            m_commandMap[observeName] = new Func<ICommand>(() => new T());
        }

        public virtual bool HasCommand(string notificationName)
        {
            return m_commandMap.ContainsKey(notificationName);
        }
        public virtual void RemoveCommand(string notificationName)
        {
            if (m_commandMap.ContainsKey(notificationName))
            {
                m_view.RemoveObserver(notificationName, this);
                m_commandMap.Remove(notificationName);
            }
        }
    }
}