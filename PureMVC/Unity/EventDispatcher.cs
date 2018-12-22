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


    public class EventDispatcher
    {
        protected IView m_view;
        protected IDictionary<int, List<IEventItem>> m_observerMap;
        internal Action<int, string> messageNoHandle { get; set; }
        internal Action<int, Exception> messageExceptionHandle { get; set; }
        private bool isRuning = false;
        private event Action endAction;

        public EventDispatcher()
        {
            m_observerMap = new Dictionary<int, List<IEventItem>>();
        }
        protected void NoMessageaction(int eventKey, string err)
        {
            if (messageNoHandle != null)
            {
                messageNoHandle(eventKey, err);
            }
        }
        protected void MessageException(int eventKey,Exception e)
        {
            if (messageExceptionHandle != null)
                messageExceptionHandle.Invoke(eventKey, e);
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
            if (m_observerMap.ContainsKey(key) && m_observerMap[key] == null)
            {
                m_observerMap[key] = new List<IEventItem>();
            }
            else
            {
                m_observerMap.Add(key, new List<IEventItem>() );
            }

            if(isRuning)
            {
                endAction += () => {
                    m_observerMap[key].Add(observer);
                };
            }
            else
            {
                m_observerMap[key].Add(observer);
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
        public void RemoveEvent(int key, Action action)
        {
            RemoveEvent(key, (System.Object)action);
        }
        public void RemoveEvent<T>(int key, Action<T> action)
        {
            RemoveEvent(key, (System.Object)action);
        }
        public void RemoveEvent<T1, T2>(int key, Action<T1, T2> action)
        {
            RemoveEvent(key, (System.Object)action);
        }
        public void RemoveEvent<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            RemoveEvent(key, (System.Object)action);
        }
        public void RemoveEvent<T1, T2, T3, T4>(int key, Action<T1, T2, T3, T4> action)
        {
            RemoveEvent(key, (System.Object)action);
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
        public void ExecuteEvents(int key)
        {
            if (m_observerMap.ContainsKey(key))
            {
                var list = m_observerMap[key];
                for (int i = 0; i < list.Count; i++)
                {
                    var eventItem = list[i];
                    if (eventItem is EventItem)
                    {
                        TryRunAction(key, eventItem);
                    }
                    else
                    {
                        NoMessageaction(key, "参数不正确");
                    }
                }
            }
            else
            {
                NoMessageaction(key, "未注册");
            }

            RunEndAction();
        }
        public void ExecuteEvents<T>(int key, T value)
        {
            if (m_observerMap.ContainsKey(key))
            {
                var list = m_observerMap[key];

                for (int i = 0; i < list.Count; i++)
                {
                    var eventItem = list[i];

                    if (eventItem is EventItem<T>)
                    {
                        TryRunAction(key, eventItem, value);
                    }
                    else if (eventItem is EventItem)
                    {
                        TryRunAction(key, eventItem);
                    }
                    else
                    {
                        NoMessageaction(key, "参数不正确");
                    }
                }
            }
            else
            {
                NoMessageaction(key, "未注册");
            }
            RunEndAction();
        }
        public void ExecuteEvents<T1, T2>(int key, T1 value1, T2 value2)
        {
            if (m_observerMap.ContainsKey(key))
            {
                var list = m_observerMap[key];

                for (int i = 0; i < list.Count; i++)
                {
                    var eventItem = list[i];

                    if (eventItem is EventItem<T1, T2>)
                    {
                        TryRunAction(key, eventItem, value1, value2);
                    }
                    else if (eventItem is EventItem<T1>)
                    {
                        TryRunAction(key, eventItem, value1);
                    }
                    else if (eventItem is EventItem)
                    {
                        TryRunAction(key, eventItem);
                    }
                    else
                    {
                        NoMessageaction(key, "参数不正确");
                    }
                }
            }
            else
            {
                NoMessageaction(key, "未注册");
            }
            RunEndAction();
        }
        public void ExecuteEvents<T1, T2, T3>(int key, T1 value1, T2 value2, T3 value3)
        {
            if (m_observerMap.ContainsKey(key))
            {
                var list = m_observerMap[key];

                for (int i = 0; i < list.Count; i++)
                {
                    var eventItem = list[i];
                    if (eventItem is EventItem<T1, T2, T3>)
                    {
                        TryRunAction(key, eventItem, value1, value2, value3);
                    }
                    else if (eventItem is EventItem<T1, T2>)
                    {
                        TryRunAction(key, eventItem, value1, value2);
                    }
                    else if (eventItem is EventItem<T1>)
                    {
                        TryRunAction(key, eventItem, value1);
                    }
                    else if (eventItem is EventItem)
                    {
                        TryRunAction(key, eventItem);
                    }
                    else
                    {
                        NoMessageaction(key, "参数不正确");
                    }
                }
            }
            else
            {
                NoMessageaction(key, "未注册");
            }
            RunEndAction();
        }
        public void ExecuteEvents<T1, T2, T3,T4>(int key, T1 value1, T2 value2, T3 value3, T4 value4)
        {
            if (m_observerMap.ContainsKey(key))
            {
                var list = m_observerMap[key];

                for (int i = 0; i < list.Count; i++)
                {
                    var eventItem = list[i];
                    if (eventItem is EventItem<T1, T2, T3,T4>)
                    {
                        TryRunAction(key, eventItem, value1, value2, value3,value4);
                    }
                    else if (eventItem is EventItem<T1, T2, T3>)
                    {
                        TryRunAction(key, eventItem, value1, value2,value3);
                    }
                    else if (eventItem is EventItem<T1, T2>)
                    {
                        TryRunAction(key, eventItem, value1,value2);
                    }
                    else if (eventItem is EventItem<T1>)
                    {
                        TryRunAction(key, eventItem, value1);
                    }
                    else if (eventItem is EventItem)
                    {
                        TryRunAction(key, eventItem);
                    }
                    else
                    {
                        NoMessageaction(key, "参数不正确");
                    }
                }
            }
            else
            {
                NoMessageaction(key, "未注册");
            }
            RunEndAction();
        }
        #endregion

        #region TryRunActionInternal
        private void TryRunAction(int eventKey, IEventItem eventItem)
        {
            isRuning = true;
            try
            {
                (eventItem as EventItem).action.Invoke();
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }

        private void TryRunAction<T>(int eventKey, IEventItem eventItem,T value)
        {
            isRuning = true;
            try
            {
                (eventItem as EventItem<T>).action.Invoke(value);
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }

        private void TryRunAction<T1, T2>(int eventKey, IEventItem eventItem, T1 value1, T2 value2)
        {
            isRuning = true;
            try
            {
                (eventItem as EventItem<T1, T2>).action.Invoke(value1, value2);
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }
        private void TryRunAction<T1, T2, T3>(int eventKey, IEventItem eventItem, T1 value1, T2 value2, T3 value3)
        {
            isRuning = true;
            try
            {
                (eventItem as EventItem<T1, T2, T3>).action.Invoke(value1, value2, value3);
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }
        private void TryRunAction<T1,T2,T3,T4>(int eventKey, IEventItem eventItem, T1 value1,T2 value2,T3 value3,T4 value4)
        {
            isRuning = true;
            try
            {
                (eventItem as EventItem<T1,T2,T3,T4>).action.Invoke(value1,value2,value3,value4);
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }

        private void RunEndAction()
        {
            Action action = endAction;
            endAction = null;
            if (action != null){
                action();
            }
        }
        #endregion

    }

}
