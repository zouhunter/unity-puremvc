using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;


namespace UnityEngine
{

    public abstract class Mediator<T> : MonoBehaviour, IMediator<T>
    {
        public virtual void OnEnable()
        {
            if (Acceptor != null || Acceptors != null)
                Facade.RegisterMediator(this);
        }
        public virtual string Acceptor { get { return null; } }

        public IList<string> Acceptors { get { return null; } }

        public abstract void HandleNotification(T notification);
        public virtual void OnDisable()
        {
            if (Acceptor != null || Acceptors != null)
                Facade.RemoveMediator(this);
        }
    }
}