using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using PureMVC;

public class Receiver : MonoBehaviour {
    void Start()
    {
        Facade.Instance.RetrieveProxy<TestProxy>("haha", OnRetrived);
    }
    void OnRetrived(TestProxy proxy)
    {
        Debug.Log(proxy.Data);
    }
    void OnGUI()
    {
        if (GUILayout.Button("改变image色"))
        {
            Facade.Instance.SendNotification<Color>("color", Color.red);
        }
    }
}
