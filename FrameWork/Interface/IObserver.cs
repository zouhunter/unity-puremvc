using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
public interface IObserver
{
    string NotifyMethod { set; }
    object NotifyContext { set; }
    void NotifyObserver(INotification notification);
    bool CompareNotifyContext(object obj);
}