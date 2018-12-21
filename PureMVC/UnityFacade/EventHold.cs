using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PureMVC
{
    public interface IEventItem
    {
        object Action { get; }
        void Release();
    }

    public class EventItem : IEventItem
    {
        public Action action;
        public object Action
        {
            get { return action; }
        }
        private static ObjectPool<EventItem> sPool = new ObjectPool<EventItem>(1, 1);

        public static EventItem Allocate(Action action)
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

    public class EventItem<T1, T2> : IEventItem
    {
        public Action<T1, T2> action;
        public object Action
        {
            get { return action; }
        }
        private static ObjectPool<EventItem<T1, T2>> sPool = new ObjectPool<EventItem<T1, T2>>(1, 1);
        public static EventItem<T1, T2> Allocate(Action<T1, T2> action)
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

    public class EventItem<T1, T2, T3> : IEventItem
    {
        public Action<T1, T2, T3> action;
        public object Action
        {
            get { return action; }
        }
        private static ObjectPool<EventItem<T1, T2, T3>> sPool = new ObjectPool<EventItem<T1, T2, T3>>(1, 1);
        public static EventItem<T1, T2, T3> Allocate(Action<T1, T2, T3> action)
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

    public class EventItem<T1, T2, T3, T4> : IEventItem
    {
        public Action<T1, T2, T3, T4> action;
        public object Action
        {
            get { return action; }
        }
        private static ObjectPool<EventItem<T1, T2, T3, T4>> sPool = new ObjectPool<EventItem<T1, T2, T3, T4>>(1, 1);
        public static EventItem<T1, T2, T3, T4> Allocate(Action<T1, T2, T3, T4> action)
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
        protected IView m_view;
        protected IDictionary<int, List<IEventItem>> m_observerMap;
        internal Action<int> messageNoHandle { get; set; }
        public EventHold()
        {
            m_observerMap = new Dictionary<int, List<IEventItem>>();
        }
        protected void NoMessageaction(int rMessage)
        {
            if (messageNoHandle != null){
                messageNoHandle(rMessage);
            }
        }

        #region 注册注销事件
        public void RegistEvent(int key, Action action)
        {
            if (action == null) return;
            EventItem observer = EventItem.Allocate(action);
            RegistEvent(key, observer);
        }
        public void RegistEvent<T>(int key, Action<T> action)
        {
            if (action == null) return;
            EventItem<T> observer = EventItem<T>.Allocate(action);
            RegistEvent(key, observer);
        }
        public void RegistEvent<T1, T2>(int key, Action<T1, T2> action)
        {
            if (action == null) return;
            EventItem<T1, T2> observer = EventItem<T1, T2>.Allocate(action);
            RegistEvent(key, observer);
        }
        public void RegistEvent<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            if (action == null) return;
            EventItem<T1, T2, T3> observer = EventItem<T1, T2, T3>.Allocate(action);
            RegistEvent(key, observer);
        }
        public void RegistEvent<T1, T2, T3, T4>(int key, Action<T1, T2, T3, T4> action)
        {
            if (action == null) return;
            EventItem<T1, T2, T3, T4> observer = EventItem<T1, T2, T3, T4>.Allocate(action);
            RegistEvent(key, observer);
        }
        private void RegistEvent(int key, IEventItem observer)
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
        #endregion

        #region 移除事件
        public void RemoveEvents(int key)
        {
            if (m_observerMap.ContainsKey(key))
            {
                m_observerMap.Remove(key);
            }
        }
        public void RemoveEvents<T>(int key, Action<T> action)
        {
            RemoveEvent(key, action);
        }
        public void RemoveEvents<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            RemoveEvent(key, action);
        }
        public void RemoveEvents<T1, T2, T3, T4>(int key, Action<T1, T2, T3, T4> action)
        {
            RemoveEvent(key, action);
        }
        private bool RemoveEvent(int key, object action)
        {
            if (action == null) return false;
            if (m_observerMap.ContainsKey(key))
            {
                var list = m_observerMap[key];
                var item = list.Find(x => object.Equals(x.Action, action));
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
        #endregion

        #region 触发事件
        public void InvokeEvents(int key)
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
                        NoMessageaction(key);
                    }
                }

            }
            else
            {
                NoMessageaction(key);
            }
        }
        public void InvokeEvents<T>(int key, T value)
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
                NoMessageaction(key);
            }
        }
        #endregion



    }

}
