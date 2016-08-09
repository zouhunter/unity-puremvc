using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
public class Mediator<T> : MonoBehaviour, IMediator<T>
{
    public virtual void OnEnable()
    {
        m_view.RegisterMediator(this);
    }
    public virtual void OnDisable()
    {
        m_view.RemoveMediator(name);
    }

    public virtual IList<string> ListNotificationInterests()
    {
        return new List<string>();
    }
    public virtual void HandleNotification(INotification<T> notification) { }
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