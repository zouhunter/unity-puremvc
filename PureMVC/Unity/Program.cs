
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
namespace PureMVC.Unity
{
    public abstract class Program<GameManager>: MonoBehaviour where GameManager: DirectManagement<GameManager>,new()
    {
        protected DirectManagement<GameManager> directManager;

        protected virtual void Awake()
        {
            directManager =  DirectManagement<GameManager>.Instence;
            directManager.RegistProgram(this);
        }
        protected virtual void OnDestroy()
        {
            directManager.RemoveProgram(this);
        }
        protected virtual void OnApplicationQuit()
        {
            directManager.OnApplicationQuit();
        }
    }

}