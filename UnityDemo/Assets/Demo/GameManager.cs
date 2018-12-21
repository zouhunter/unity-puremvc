using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;

public class GameManager : Director<GameManager>
{
    protected override void OnFrameWorkLunched()
    {
        Debug.Log("OnFrameWorkLunched!");
        RegisterProxy(new ColorProxy(ProxyName.COLOR_BLOCK_GIRL,Color.red,Color.yellow,Color.cyan));
        RegisterProxy(new ColorProxy(ProxyName.COLOR_BLOCK_BOY,Color.blue,Color.green,Color.gray));
        RegisterCommand<ChangeColorCommand,int>(ObserverName.CHANGE_COLOR);
    }
}
