using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
public abstract class Mediator : IMediator
{
    private string mediatorName;
    public abstract string[] ListNotificationInterests();
    public abstract void HandleNotification(INotification notification);

    public virtual void OnRegister() { }
    public virtual void OnRemove() { }
    public virtual string MediatorName
    {
        get {
            if (mediatorName == null){
                mediatorName = System.Guid.NewGuid().ToString();
            }
            return mediatorName;
        }
    }

    private IView m_view;
    public Mediator()
    {
        m_view = View.Instance;
    }
}

public abstract class Mediator<T> : IMediator<T>
{
    private string mediatorName;
    public abstract string[] ListNotificationInterests ();
	public abstract void HandleNotification (INotification<T> notification);

    public virtual void OnRegister() { }
    public virtual void OnRemove() { }
    public virtual string MediatorName
    {
        get
        {
            if (mediatorName == null)
            {
                mediatorName = System.Guid.NewGuid().ToString();
            }
            return mediatorName;
        }
    }

    private IView m_view;
    public Mediator()
    {
        m_view = View.Instance;
    }
}