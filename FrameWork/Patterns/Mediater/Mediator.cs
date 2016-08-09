using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
public class Mediator : MonoBehaviour, IMediator
{
    public virtual IList<NotiConst> ListNotificationInterests()
    {
        return new List<NotiConst>();
    }
    public virtual void HandleNotification(INotification notification) { }
    public virtual void OnRegister()
    {
        //Debug.Log(name + "Register");
    }
    public virtual void OnRemove()
    {
        //Debug.Log(name + "Remove");
    }
    public virtual string MediatorName
    {
        get { return gameObject.name; }
    }
}