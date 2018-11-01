using UnityEngine;
using System.Reflection;
using System;

namespace PureMVC
{
    public abstract class App<T> : Facade where T : App<T>,new()
    {
        protected static T instance = default(T);
        private static object lockHelper = new object();
        private static bool isQuit = false;
        private bool isOn = false;
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
                isOn = true;
            }
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