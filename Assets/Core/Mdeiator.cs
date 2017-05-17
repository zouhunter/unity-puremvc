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
            Facade.RegisterMediator(this);
        }
        public abstract IList<string> ListNotificationInterests();
        public abstract void HandleNotification(T notification);
        public virtual void OnDisable()
        {
            Facade.RemoveMediator(this);
        }
    }
}