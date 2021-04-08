using System;
using System.Collections.Generic;
namespace PureMVC
{
    public class Controller : IController
    {
        protected IView m_view;
        protected IDictionary<int, Func<ICommandpublic>> m_commandMap;

        public Controller(IView view)
        {
            m_view = view;
            m_commandMap = new Dictionary<int, Func<ICommandpublic>>();
        }

        /// <summary>
        /// 注册非泛型命令
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="newType"></param>
        public virtual void RegisterCommand<T>(int observeKey) where T : ICommand, new()
        {
            if (typeof(T).BaseType.IsGenericType)
            {
                Console.WriteLine("请注册" + observeKey + "的参数类型" +
                      "\n否则不会调用带参数的方法");
            }

            if (!m_commandMap.ContainsKey(observeKey))
            {
                IObserver observer = new Observer((notification) =>
                {
                    Func<ICommandpublic> acceptor;
                    if (m_commandMap.TryGetValue(notification.ObserverKey, out acceptor))
                    {
                        var instence = acceptor();
                        if (instence is ICommand) (acceptor() as ICommand).Execute();
                        else
                        {
                            Console.WriteLine("命令" + observeKey + "参数不正确，无法执行");
                        }
                    }
                }, this);
                m_view.RegisterObserver(observeKey, observer);
                m_commandMap[observeKey] = new Func<ICommandpublic>(() => new T());
            }
            else
            {
                Console.WriteLine("已经注册" + observeKey + "的命令" + "->不能重复注册");
            }
        }
        /// <summary>
        /// 注册泛型命令
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="notificationName"></param>
        /// <param name="newcommandFunc"></param>
        public virtual void RegisterCommand<T, P>(int observeKey) where T : ICommand<P>, new()
        {
            if (!m_commandMap.ContainsKey(observeKey))
            {
                IObserver<P> observer = new Observer<P>((notification) =>
                {
                    Func<ICommandpublic> acceptor;
                    if (m_commandMap.TryGetValue(notification.ObserverKey, out acceptor))
                    {
                        var instence = acceptor();
                        if (instence is ICommand<P>) (instence as ICommand<P>).Execute(notification.Body);
                        else
                        {
                            Console.WriteLine("命令" + observeKey + "参数不正确，无法执行");
                        }
                    }
                }, this);
                m_view.RegisterObserver(observeKey, observer);
                m_commandMap[observeKey] = new Func<ICommandpublic>(() => new T());
            }
            else
            {
                Console.WriteLine("已经注册" + observeKey + "的命令" + "->不能重复注册");
            }
        }

        public virtual bool HaveCommand(int notificationName)
        {
            return m_commandMap.ContainsKey(notificationName);
        }
        public virtual void RemoveCommand(int notificationName)
        {
            if (m_commandMap.ContainsKey(notificationName))
            {
                m_view.RemoveObserver(notificationName, this);
                m_commandMap.Remove(notificationName);
            }
        }
    }
}