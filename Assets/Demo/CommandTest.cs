using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class MyCommand1 : ICommand
{
    public void Execute()
    {
        MonoBehaviour.FindObjectOfType<Text>().text = "commond1";
    }
}
public class MyCommand2 : ICommand<string>
{
    public void Execute(string value)
    {
        MonoBehaviour.FindObjectOfType<Text>().text = "commond2";
    }
}

public class CommandTest : MonoBehaviour
{
    private void Awake()
    {
        Facade.RegisterCommand<MyCommand1>("command1");
        Facade.RegisterCommand<MyCommand2, string>("command2");
    }

    private void OnGUI()
    {

        {

        }
    }
}
