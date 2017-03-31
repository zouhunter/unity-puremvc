using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
public abstract class Mediator : MonoBehaviour, IMediator
{
    private string mediatorName;
    public virtual string MediatorName
    {
        get {
            if (mediatorName == null){
                mediatorName = System.Guid.NewGuid().ToString();
            }
            return mediatorName;
        }
    }
    public virtual void OnEnable()
    {
        Facade.Instance.RegisterMediator(this);
    }
    public abstract IList<string> ListNotificationInterests();
    public abstract void HandleNotification(INotification notification);

    public virtual void OnRegister() { }
    public virtual void OnRemove() { }
    public virtual void OnDisable()
    {
        Facade.Instance.RemoveMediator(this);
    }
}

public abstract class Mediator<T> :MonoBehaviour, IMediator<T>
{
    public virtual void OnEnable()
    {
        Facade.Instance.RegisterMediator(this);
    }
    public abstract IList<string> ListNotificationInterests ();
	public abstract void HandleNotification (INotification<T> notification);
    public virtual void OnDisable()
    {
        Facade.Instance.RemoveMediator(this);
    }
}