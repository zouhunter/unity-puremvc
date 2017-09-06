using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Controller : IController
    {
        protected IView m_view;
        protected IDictionary<string, ICommand> m_commandMap;
        protected static volatile IController m_instance;
        protected Controller()
        {
            m_commandMap = new Dictionary<string, ICommand>();
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
        public virtual void RegisterCommand<T>() where T : ICommand,new()
        {
            var commandInstance = new T();

            if (commandInstance.GetType().BaseType.IsGenericType)
            {
                Debug.Log("请注册" + commandInstance + "的参数类型" +
                    "\n否则不会调用带参数的方法");
            }

            var notificationName = commandInstance.Acceptor;
            if (!m_commandMap.ContainsKey(notificationName))
            {
                IObserver observer = new Observer((notification) => {
                    ICommand acceptor;
                    if (m_commandMap.TryGetValue(notification.ObserverName, out acceptor))
                    {
                        (commandInstance as ICommand).Execute();
                    }
                }, this);
                m_view.RegisterObserver(notificationName, observer);
            }
            m_commandMap[notificationName] = commandInstance;
        }
        /// <summary>
        /// 注册泛型命令
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="notificationName"></param>
        /// <param name="newcommandFunc"></param>
        public virtual void RegisterCommand<T,P>() where T : ICommand<P>, new()
        {
            var commandInstance = new T();
            var notificationName = commandInstance.Acceptor;

            if (!m_commandMap.ContainsKey(notificationName))
            {
                IObserver<P> observer = new Observer<P>((notification) => {
                    ICommand type;
                    if (m_commandMap.TryGetValue(notification.ObserverName, out type))
                    {
                         (commandInstance as ICommand<P>).Execute(notification.Body);
                         (commandInstance as ICommand).Execute();
                    }
                }, this);
                m_view.RegisterObserver(notificationName, observer);
            }
            m_commandMap[notificationName] = commandInstance;
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