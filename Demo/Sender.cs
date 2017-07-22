using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class Sender : MonoBehaviour {
  
    void OnGUI()
    {
        if (GUILayout.Button("Mediator"))
        {
            Facade.SendNotification<Color>("color", Color.red);
        }
        if (GUILayout.Button("Command1"))
        {
            Facade.SendNotification("command1");
        }
        if (GUILayout.Button("Command2"))
        {
            Facade.SendNotification(typeof(MyCommand2).ToString(), "哈哈");
        }
        if (GUILayout.Button("Proxy"))
        {
            Facade.RetrieveProxy<string>("haha", OnRetrived);
        }
    }
    void OnRetrived(string proxy)
    {
        Debug.Log(proxy);
        FindObjectOfType<Text>().text = proxy;
    }
}
