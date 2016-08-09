using UnityEngine;
using System.Collections;
using System;

public class Notification<T> : INotification<T>
{
    public Notification(string observerName)
    {
        ObserverName = observerName;
    }
    public Notification(string observerName, T body)
    {
        this.ObserverName = observerName;
        this.Body = body;
    }
    public Notification(string observerName, T body, Type type)
    {
        ObserverName = observerName;
        Type = type;
        Body = body;
    }
    public T Body { get; set; }
    public Type Type { get; set; }
    public string ObserverName { get; set; }
    public new string ToString
    {
        get
        {
            string msg = "";
            msg += "\nObserverName:" + ObserverName.ToString();
            msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
            msg += "\nType:" + ((Type == null) ? "null" : Type.ToString());
            return msg;
        }
    }

}