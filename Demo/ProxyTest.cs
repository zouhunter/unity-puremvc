using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class ProxyTest : MonoBehaviour {

    void Start()
    {
        var proxy = new Proxy<string>("haha");
        proxy.Data = "dddddddddd";
        Facade.RegisterProxy(proxy);
    }
  
}
