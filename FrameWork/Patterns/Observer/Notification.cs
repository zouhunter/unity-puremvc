using UnityEngine;
using System.Collections;
using System;

public class Notification : INotification
{
    public Notification (NotiConst noti)
    {
        ObserverName = noti;
    }
    public Notification (NotiConst name,object body)
    {
        this.ObserverName = name;
        this.Body = body;
    }
    public Notification (NotiConst name, object body, Type type)
    {
        ObserverName = name;
        Type = type;
        Body = body;
    }
    public object Body { get; set; }
    public Type Type { get; set; }
    public NotiConst ObserverName { get; set; }
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