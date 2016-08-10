using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Core;

namespace PureMVC
{
    public abstract class Mediator<T> : Notifyer, IMediator<T>
    {
        public abstract string[] ListNotificationInterests();
        public abstract void HandleNotification(INotification<T> notification);

        public virtual void OnRegister() { }
        public virtual void OnRemove() { }
        public virtual string MediatorName
        {
            get { return mediatorName; }
        }
        private string mediatorName;

        public Mediator(string name)
        {
            mediatorName = name;
        }
    }
    public abstract class Mediator<T,S> : Notifyer, IMediator<T,S>
    {
        public abstract string[] ListNotificationInterests();
        public abstract void HandleNotification(INotification<T> notification);

        public virtual void OnRegister() { }
        public virtual void OnRemove() { }
        public virtual string MediatorName
        {
            get { return mediatorName; }
        }

        public S Component { get
            { return m_Component; }
        }

        private string mediatorName;
        private S m_Component;

        public Mediator(string name,S component)
        {
            mediatorName = name;
            m_Component = component;
        }
    }


}