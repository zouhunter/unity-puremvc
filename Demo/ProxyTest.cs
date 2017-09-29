using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
public class MyProxy : Proxy<string>
{
    public MyProxy(string proxyName, string data) : base(proxyName, data) { }
}
public class ProxyTest : MonoBehaviour {

    void Start()
    {
        var proxy = new Proxy<string>("haha");
        proxy.Data = "dddddddddd";
        Facade.RegisterProxy(proxy);
        Facade.RegisterProxy(new MyProxy("hehe", "bbbbbb"));
    }
  
}
