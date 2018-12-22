using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Unity;
using System;

public class GameManager : StaticDirectManagement<GameManager>
{
    protected override void OnFrameWorkLunched()
    {
        Regist(new ColorProxy(ProxyName.COLOR_BLOCK_GIRL, Color.red, Color.yellow, Color.cyan));
        Regist(new ColorProxy(ProxyName.COLOR_BLOCK_BOY, Color.blue, Color.green, Color.gray));
        Regist<ChangeColorCommand, int>(ObserverName.CHANGE_COLOR);
    }
    protected override void OnApplicationQuit()
    {
    }
}
