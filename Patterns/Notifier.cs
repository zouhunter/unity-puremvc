using System;
using UnityEngine;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Core;

namespace PureMVC
{
    public class Notifyer : INotifier
    {
        protected IFacade Facade{
            get {
                if (m_Facade == null)
                {
                    m_Facade = PureMVC.Facade.Instance;
                }
                return m_Facade;
            }
        }
        private IFacade m_Facade;

        /// <summary>
        /// 通知观察者
        /// </summary>
        /// <param name="notification"></param>
        public void NotifyObservers<T>(INotification<T> notification)
        {
            Facade.NotifyObservers<T>(notification);
        }
        public void SendNotification(string observeName)
        {
            NotifyObservers(new Notification<object>(observeName));
        }
        public void SendNotification<T>(string observeName, T body)
        {
            NotifyObservers(new Notification<T>(observeName, body));
        }
        public void SendNotification<T>(string observeName, T body, Type type)
        {
            NotifyObservers(new Notification<T>(observeName, body, type));
        }
    }
}
