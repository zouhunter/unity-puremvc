using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class Mediator : Notifier, IMediator
{
    public virtual IList<NotiConst> ListNotificationInterests()
    {
        return new List<NotiConst>();
    }
    public virtual void HandleNotification(INotification notification)
    {
    }
    public virtual void OnRegister()
    {
    }
    public virtual void OnRemove()
    {
    }
    public virtual string MediatorName
    {
        get { return gameObject.name; }
    }
    public virtual Component ViewComponent
    {
        get { return m_viewComponent; }
        set { m_viewComponent = value; }
    }
    protected Component m_viewComponent;
}