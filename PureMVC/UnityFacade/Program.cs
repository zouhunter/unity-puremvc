
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
namespace PureMVC
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

        protected GameObjectPool _container;

        protected virtual void Awake()
        {
            _abstruct = this;
            _container = new GameObjectPool(this);
        }

        #region  访问对象池
        public GameObject GetPoolObject(GameObject pfb, Transform parent, bool world, bool resetLocalPosition = true, bool resetLocalScale = false, bool activepfb = false)
        {
            return _container.GetPoolObject(pfb, parent, world, resetLocalPosition, resetLocalScale, activepfb);
        }
        public GameObject GetPoolObject(string pfbName, Transform parent, bool world, bool resetLocalPosition = true, bool resetLocalScale = false)
        {
            return _container.GetPoolObject(pfbName, parent, world, resetLocalPosition, resetLocalScale);
        }
        public void SavePoolObject(GameObject go, bool world = false)
        {
            _container.SavePoolObject(go, false);
        }
        #endregion
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