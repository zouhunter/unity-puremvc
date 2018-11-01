using UnityEngine;
using System.Reflection;

namespace PureMVC
{
    public abstract class App<T> : MonoBehaviour where T : App<T>
    {
        protected static T instance = default(T);
        protected Facade facade = default(Facade);
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
                            GameObject go = new GameObject(typeof(T).ToString());
                            instance = go.AddComponent<T>();
                            Instence.OnFrameWorkLunched();
                            DontDestroyOnLoad(go);
                        }
                    }
                }
                return instance;
            }
        }
        public Facade Facade
        {
            get
            {
                if(facade == null)
                {
                    facade = new Facade();
                }
                return facade;
            }
        }
        public virtual void StartGame()
        {
            if(!isOn)
            {
                isOn = true;
                facade = CreateFacade();
            }
        }

        protected virtual Facade CreateFacade()
        {
            facade = new Facade();
            return facade;
        }

        protected virtual void OnApplicationQuit()
        {
            isOn = false;
            isQuit = true;
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
    }
}