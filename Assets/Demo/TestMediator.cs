using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

using PureMVC;
using PureMVC.Internal;

public class TestMediator : Mediator<Color>
{
    public override void HandleNotification(INotification<Color> notification)
    {
        GetComponent<Image>().color = notification.Body;
    }

    public override IList<string> ListNotificationInterests()
    {
        return new string[] { "color" };
    }
}
