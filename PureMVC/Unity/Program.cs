
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
namespace PureMVC.Unity
{
    public abstract class Program : MonoBehaviour
    {
        protected static Program _abstruct;
        public static Program Current
        {
            get
            {
                return _abstruct;
            }
        }
        public Facade facade { get; protected set; }
        protected virtual void Awake()
        {
            _abstruct = this;
        }
    }
    public abstract class Program<S>: Program where S: Director<S>,new()
    {
        protected override void Awake()
        {
            base.Awake();
            var director =  Director<S>.Instence;
            director.StartGame();
            facade = director;
        }
        protected virtual void OnApplicationQuit()
        {
             Director<S>.Instence.OnApplicationQuit(); 
        }
    }

}