using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class TestMediator : Mediator<Color>
{
    public override void HandleNotification(Color notification)
    {
        GetComponent<Image>().color = notification;
    }

    public override IList<string> ListNotificationInterests()
    {
        return new string[] { "color" };
    }
}
