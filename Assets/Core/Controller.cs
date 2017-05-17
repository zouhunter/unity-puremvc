using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Controller : IController
    {
        protected IView m_view;
        protected IDictionary<string, Type> m_commandMap;
        protected static volatile IController m_instance;
        protected Controller()
        {
            m_commandMap = new Dictionary<string, Type>();
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
        public virtual void RegisterCommand(string notificationName, Type newType)
        {
            if (!m_commandMap.ContainsKey(notificationName))
            {
                IObserver observer = new Observer((notification) => {
                    Type type;
                    if (m_commandMap.TryGetValue(notification.ObserverName, out type))
                    {
                        var commandInstance = Activator.CreateInstance(type);// commandFunc.Invoke();
                        if (commandInstance is ICommand) (commandInstance as ICommand).Execute();
                    }
                }, this);
                m_view.RegisterObserver(notificationName, observer);
            }
            m_commandMap[notificationName] = newType;
        }
        /// <summary>
        /// 注册泛型命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="notificationName"></param>
        /// <param name="newcommandFunc"></param>
        public virtual void RegisterCommand<T>(string notificationName, Type newType)
        {
            if (!m_commandMap.ContainsKey(notificationName))
            {
                IObserver<T> observer = new Observer<T>((notification) => {
                    Type type;
                    if (m_commandMap.TryGetValue(notification.ObserverName, out type))
                    {
                        var commandInstance = Activator.CreateInstance(type);// commandFunc.Invoke();
                        if (commandInstance is ICommand<T>) (commandInstance as ICommand<T>).Execute(notification.Body);
                        else if (commandInstance is ICommand) (commandInstance as ICommand).Execute();
                    }
                }, this);
                m_view.RegisterObserver(notificationName, observer);
            }
            m_commandMap[notificationName] = newType;
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