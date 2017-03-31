using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using PureMVC;
public class TestProxy : Proxy<string>
{
    public TestProxy(string name) :base(name)
    {

    }
    private string data;
    public override string Data
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }
}

public class Register : MonoBehaviour {
    void Start()
    {
        TestProxy proxy = new TestProxy("haha");
        proxy.Data = "dddddddddd";
        Facade.Instance.RegisterProxy(proxy);
    }
   
}
