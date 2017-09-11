using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class TestMediator : Mediator<object>
{
    public override void HandleNotification(object notification)
    {
        GetComponent<Image>().color = (Color)notification;
    }

    public override string Acceptor
    {
        get
        {
            return  "color" ;
        }
    }
}
