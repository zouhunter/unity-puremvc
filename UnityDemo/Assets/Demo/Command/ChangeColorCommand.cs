using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;

public class ChangeColorCommand : Command<int>
{
    private int[] colorKeys = { ProxyName.COLOR_BLOCK_GIRL, ProxyName.COLOR_BLOCK_BOY };

    public override void Execute(int id)
    {
        if (colorKeys.Length < id || id < 0) return;

        var proxy = GameManager.Retrive_Proxy<Color[]>(colorKeys[id]);

        for (int i = 0; i < proxy.Data.Length; i++)
        {
            var color = proxy.Data[i];
            GameManager.Notify(ObserverName.CHANGE_COLOR_UI, new KeyValuePair<int, Color>(i, color));
        }
    }
}