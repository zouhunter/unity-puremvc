using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class MyCommand1 : ICommand
{
    public string Acceptor
    {
        get
        {
            return "command1";
        }
    }

    public void Execute()
    {
        MonoBehaviour.FindObjectOfType<Text>().text = "commond1";
    }
}
public class MyCommand2 : ICommand<string>
{
    public string Acceptor
    {
        get
        {
            return "command2";
        }
    }

    public void Execute(string value)
    {
        MonoBehaviour.FindObjectOfType<Text>().text = "commond2";
    }
}

public class CommandTest : MonoBehaviour
{
    private void Awake()
    {
        Facade.RegisterCommand<MyCommand1>();
        Facade.RegisterCommand<MyCommand2, string>();
    }
}
