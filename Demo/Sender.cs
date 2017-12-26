using System;

using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Sender : SceneMain<Sender> {
  
    void OnGUI()
    {
        if (GUILayout.Button("Mediator"))
        {
            Facade.SendNotification<Color>(ObserverName.FirstMediator, Color.red);
        }
        if (GUILayout.Button("Command1"))
        {
            Facade.SendNotification(ObserverName.Command1);
        }
        if (GUILayout.Button("Command2"))
        {
            Facade.SendNotification(ObserverName.Command2, "哈哈");
        }
        if (GUILayout.Button("Proxy"))
        {
            Facade.RetrieveData<string>(ProxyName.FirstProxy, OnRetrived);
            Facade.RetrieveProxy<MyProxy,string>(ProxyName.SecondProxy, OnRetrived);
        }
        if (GUILayout.Button("Event"))
        {
            SceneMain.Current.InvokeEvents(EventKey.FirstEvent,"事件触发创建的cube");
        }
    }
    void OnRetrived(string proxy)
    {
        Debug.Log(proxy);
        FindObjectOfType<Text>().text = proxy;
    }
    void OnRetrived(MyProxy myProxy)
    {
        Debug.Log(myProxy.Data);
    }
}
