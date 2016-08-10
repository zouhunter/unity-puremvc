using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using PureMVC.Interfaces;

namespace PureMVC.Core
{
    public class Controller : IController
    {
        protected IView m_view;
        protected IDictionary<string, ICommand> m_commandMap;

        protected readonly object m_syncRoot = new object();
        protected readonly static object m_StaticSyncRoot = new object();
        public static IController Instance = new Controller();
        public Controller()
        {
            m_commandMap = new Dictionary<string, ICommand>();
            InitializeController();
        }
        protected virtual void InitializeController()
        {
            m_view = View.Instance;
        }

        public void RegisterCommand(string observerName, ICommand command)
        {
            lock (m_syncRoot)
            {
                if (!m_commandMap.ContainsKey(observerName))
                {
                    m_view.RegisterObserver(observerName, new Observer("Execute", command));
                    m_commandMap.Add(observerName, command);
                }
            }
        }
        public bool HasCommand(string observerName)
        {
            lock (m_syncRoot)
            {
                return m_commandMap.ContainsKey(observerName);
            }
        }
        public ICommand RemoveCommand(string notificationName)
        {
            lock (m_syncRoot)
            {
                ICommand type = null;
                if (m_commandMap.ContainsKey(notificationName))
                {
                    type = m_commandMap[notificationName];
                    m_view.RemoveObserver(notificationName, this);
                    m_commandMap.Remove(notificationName);
                }
                return type;
            }
        }
    }
}