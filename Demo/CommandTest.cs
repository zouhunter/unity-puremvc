using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class MyCommand1 : Command
{
    public override void Execute()
    {
        MonoBehaviour.FindObjectOfType<Text>().text = "commond1";
    }
}
public class MyCommand2 : Command<string>
{
    public override void Execute(string value)
    {
        MonoBehaviour.FindObjectOfType<Text>().text = "commond2" + value;
    }
}

public class CommandTest : MonoBehaviour
{
    private void Awake()
    {
        Facade.RegisterCommand<MyCommand1>(ObserverName.Command1);
        Facade.RegisterCommand<MyCommand2, string>(ObserverName.Command2);
    }
}
