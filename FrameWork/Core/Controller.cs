using System;
using System.Collections.Generic;
using UnityEngine;
public class Controller : IController
{
    protected IView m_view;
    protected IDictionary<string, Type> m_commandMap;

    public static IController Instance = new Controller();
    protected readonly object m_syncRoot = new object();

    public Controller()
    {
        m_commandMap = new Dictionary<string, Type>();
        InitializeController();
    }
    protected virtual void InitializeController()
    {
        m_view = View.Instance;
    }

    public virtual void RegisterCommand(string observerName, Type commandType)
    {
        lock (m_syncRoot)
        {
            if (!m_commandMap.ContainsKey(observerName))
            {
                m_view.RegisterObserver(observerName, new Observer("ExecuteCommand", this));
                m_commandMap.Add(observerName, commandType);
            }
        }
    }
    /// <summary>
    /// 执行Command
    /// </summary>
    /// <param name="note"></param>
    public virtual void ExecuteCommand(INotification note)
    {
        Type commandType = null;

        lock (m_syncRoot)
        {
            if (!m_commandMap.ContainsKey(note.ObserverName)) return;
            commandType = m_commandMap[note.ObserverName];
        }

        object commandInstance = Activator.CreateInstance(commandType);

        if (commandInstance is ICommand)
        {
            ((ICommand)commandInstance).Execute(note);
        }
    }
    public virtual bool HasCommand(string observerName)
    {
        lock (m_syncRoot)
        {
            return m_commandMap.ContainsKey(observerName);
        }
    }
    public virtual Type RemoveCommand(string notificationName)
    {
        lock (m_syncRoot)
        {
            Type type = null;
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