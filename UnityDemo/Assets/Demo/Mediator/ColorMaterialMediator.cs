using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;

public class ColorMaterialMediator : Mediator<KeyValuePair<int, Color>>
{
    private int key;
    private Renderer render;
    public override IList<int> Acceptors
    {
        get
        {
            return new int[] { ObserverName.CHANGE_COLOR_UI };
        }
    }

    public ColorMaterialMediator(int key, Renderer render)
    {
        this.key = key;
        this.render = render;
    }

    public override void HandleNotification(int observerName, KeyValuePair<int, Color> notification)
    {
        if (notification.Key == key)
        {
            if (render == null || render.material == null)
            {
                GameManager.Instence.RemoveMediator(this);
            }
            else
            {
                render.material.color = notification.Value;
            }
        }
    }
}
