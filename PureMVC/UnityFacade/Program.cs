
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
namespace PureMVC
{
    public abstract class Program : MonoBehaviour
    {
        public interface IEventItem
        {
            object Action { get; }
            void Release();
        }

        public class EventItem : IEventItem
        {
            public UnityAction action;
            public object Action
            {
                get { return action; }
            }
            private static ObjectPool<EventItem> sPool = new ObjectPool<EventItem>(1, 1);

            public static EventItem Allocate(UnityAction action)
            {
                var item = sPool.Allocate();
                item.action = action;
                return item;
            }
            public void Release()
            {
                sPool.Release(this);
            }
        }

        public class EventItem<T> : IEventItem
        {
            public Action<T> action;
            public object Action
            {
                get { return action; }
            }
            private static ObjectPool<EventItem<T>> sPool = new ObjectPool<EventItem<T>>(1, 1);

            public static EventItem<T> Allocate(Action<T> action)
            {
                var item = sPool.Allocate();
                item.action = action;
                return item;
            }
            public void Release()
            {
                sPool.Release(this);
            }
        }

        public class EventHold
        {
            protected IDictionary<string, List<IEventItem>> m_observerMap;

            public Action<string> MessageNotHandled { get; set; }
            public EventHold()
            {
                m_observerMap = new Dictionary<string, List<IEventItem>>();
            }
            public void NoMessageHandle(string rMessage)
            {
                if (MessageNotHandled == null)
                {
                    Debug.LogWarning("MessageDispatcher: Unhandled Message of type " + rMessage);
                }
                else
                {
                    MessageNotHandled(rMessage);
                }
            }

            #region 注册注销事件
            public void AddDelegate<T>(string key, Action<T> handle)
            {
                if (handle == null) return;
                EventItem<T> observer = EventItem<T>.Allocate(handle);
                RegisterObserver(key, observer);
            }
            public void AddDelegate(string key, UnityAction handle)
            {
                if (handle == null) return;
                EventItem observer = EventItem.Allocate(handle);
                RegisterObserver(key, observer);
            }

            public void RemoveDelegate<T>(string key, Action<T> handle)
            {
                ReMoveObserver(key, handle);
            }
            public void RemoveDelegate(string key, UnityAction handle)
            {
                ReMoveObserver(key, handle);
            }
            public void RemoveDelegates(string key)
            {
                if (m_observerMap.ContainsKey(key))
                {
                    m_observerMap.Remove(key);
                }
            }
            #endregion

            #region 触发事件
            public void NotifyObserver(string key)
            {
                if (m_observerMap.ContainsKey(key))
                {
                    var list = m_observerMap[key];
                    foreach (var item in list)
                    {
                        if (item is EventItem)
                        {
                            (item as EventItem).action.Invoke();
                        }
                        else
                        {
                            NoMessageHandle(key);
                        }
                    }

                }
                else
                {
                    NoMessageHandle(key);
                }
            }
            public void NotifyObserver<T>(string key, T value)
            {
                if (m_observerMap.ContainsKey(key))
                {
                    var list = m_observerMap[key];
                    var actions = list.FindAll(x => x is EventItem<T>);
                    foreach (var item in actions)
                    {
                        (item as EventItem<T>).action.Invoke(value);
                    }
                }
                else
                {
                    NoMessageHandle(key);
                }
            }
            #endregion

            private void RegisterObserver(string key, IEventItem observer)
            {
                if (m_observerMap.ContainsKey(key))
                {
                    if (!m_observerMap[key].Contains(observer))
                    {
                        m_observerMap[key].Add(observer);
                    }
                }
                else
                {
                    m_observerMap.Add(key, new List<IEventItem>() { observer });
                }
            }

            private bool ReMoveObserver(string key, object handle)
            {
                if (handle == null) return false;
                if (m_observerMap.ContainsKey(key))
                {
                    var list = m_observerMap[key];
                    var item = list.Find(x => object.Equals(x.Action, handle));
                    if (item != null)
                    {
                        item.Release();
                        list.Remove(item);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        protected static Program _abstruct;

        public static Program Current
        {
            get
            {
                return _abstruct;
            }
        }
        private ObjectPool _container;
        private EventHold _eventHold;
        protected virtual void Awake()
        {
            _eventHold = new EventHold();
            _container = new ObjectPool(this);
            _abstruct = this;
        }

        #region 访问事件系统
        public void RegisterEvent(string noti, UnityAction even)
        {
            _eventHold.AddDelegate(noti, even);
        }
        public void RegisterEvent<T>(string noti, Action<T> even)
        {
            _eventHold.AddDelegate(noti, even);
        }

        public void RemoveEvent(string noti, UnityAction even)
        {
            _eventHold.RemoveDelegate(noti, even);
        }
        public void RemoveEvent<T>(string noti, Action<T> even)
        {
            _eventHold.RemoveDelegate(noti, even);
        }

        public void RemoveEvents(string noti)
        {
            _eventHold.RemoveDelegates(noti);
        }

        public void Notify(string noti)
        {
            _eventHold.NotifyObserver(noti);
        }
        public void Notify<T>(string noti, T data)
        {
            _eventHold.NotifyObserver<T>(noti, data);
        }
        #endregion

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

    public abstract class Program<S> : Program where S:App<S>,new()
    {
        protected override void Awake()
        {
            base.Awake();
            var app = App<S>.Instence;
            app.StartGame();
        }

        protected virtual void OnApplicationQuit()
        {
            App<S>.Instence.OnApplicationQuit(); 
        }
    }

}