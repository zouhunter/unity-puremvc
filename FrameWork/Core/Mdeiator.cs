using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
public abstract class Mediator<T> : MonoBehaviour, IMediator<T>
{
    public virtual void OnEnable()
    {
        m_view.RegisterMediator(this);
    }
    public virtual void OnDisable()
    {
        m_view.RemoveMediator(name);
    }

	public abstract IList<ObserverName> ListNotificationInterests ();
	public abstract void HandleNotification (INotification<T> notification);

    public virtual void OnRegister() { }
    public virtual void OnRemove() { }
    public virtual string MediatorName
    {
        get { return gameObject.name; }
    }

    private IView m_view;
    public Mediator()
    {
        m_view = View.Instance;
    }
}