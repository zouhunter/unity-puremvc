using UnityEngine;
using System.Reflection;
using System;

namespace PureMVC
{
    public abstract class  Director<T> : Facade where T :  Director<T>,new()
    {
        protected static T instance = default(T);
        private static object lockHelper = new object();
        private static bool isQuit = false;
        private bool isOn = false;
        private EventHold _eventDispatch;
        public EventHold eventDispatch { get { return _eventDispatch; } }
        public static T Instence
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null && !isQuit)
                        {
                            instance = new T();
                            Instence.OnFrameWorkLunched();
                        }
                    }
                }
                return instance;
            }
        }
        internal void StartGame()
        {
            if(!isOn)
            {
                notifyNotHandle = OnNotifyNotHandle;

                _eventDispatch = new EventHold();
                _eventDispatch.messageNoHandle = OnEventNotHandled;

                isOn = true;
            }
        }
        protected virtual void OnEventNotHandled(int eventID)
        {
            Debug.LogWarning("【Unhandled Event】 ID: " + eventID);
        }
        protected virtual void OnNotifyNotHandle(int observerID)
        {
            Debug.LogWarning("【Unhandled Notify】 ID: " + observerID);
        }
        protected abstract void OnFrameWorkLunched();
        protected static void InitProperties<S>()
        {
            var fields = typeof(S).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
            foreach (var item in fields)
            {
                item.SetValue(null, item.Name, null);
            }
        }
        internal void OnApplicationQuit()
        {
            isOn = false;
            isQuit = true;
        }
    }
}