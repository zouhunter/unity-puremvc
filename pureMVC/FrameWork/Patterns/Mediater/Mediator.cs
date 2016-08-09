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
    public virtual void HandleNotification(INotification notification)
    {
        try {
            if (ViewComponent is Text)
            {
                ((Text)ViewComponent).text = (string)notification.Body;
            }
            else if (ViewComponent is Image)
            {
                ((Image)ViewComponent).sprite = (Sprite)notification.Body;
            }
            else if (ViewComponent is Slider)
            {
                ((Slider)ViewComponent).value = (float)notification.Body;
            }
        }
        catch(UnityException e)
        {
            Debug.LogError(e);
        }
    }
    public virtual void OnRegister()
    {
        Debug.Log(name + "Register");
    }
    public virtual void OnRemove()
    {
        Debug.Log(name + "Remove");
    }
    public virtual string MediatorName
    {
        get { return gameObject.name; }
    }
    public virtual Component ViewComponent
    {
        get { return null; }
    }
    //public virtual T ViewComponent<T>()where T:Component
    //{
    //    return default(T);
    //}
}