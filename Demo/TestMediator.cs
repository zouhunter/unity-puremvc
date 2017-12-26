using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class TestMediator : Mediator<object>
{
    public override void HandleNotification(string observerName,object notification)
    {
        GetComponent<Image>().color = (Color)notification;
    }

    public override string Acceptor
    {
        get
        {
            return ObserverName.FirstMediator;
        }
    }
}
