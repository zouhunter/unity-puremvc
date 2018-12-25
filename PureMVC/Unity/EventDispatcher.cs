using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PureMVC
{
    public class EventDispatcher
    {
        protected readonly IDictionary<int, List<MulticastDelegate>> m_observerMap;
        internal Action<int, string> messageNoHandle { get; set; }
        internal Action<int, Exception> messageExceptionHandle { get; set; }
        protected bool isRuning = false;
        protected event Action endAction;

        public EventDispatcher()
        {
            m_observerMap = new Dictionary<int, List<MulticastDelegate>>();
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
            RegistEvent(key, action);
        }
        public void RegistEvent<T>(int key, Action<T> action)
        {
            if (action == null) return;
            RegistEvent(key, action);
        }
        public void RegistEvent<T1, T2>(int key, Action<T1, T2> action)
        {
            if (action == null) return;
            RegistEvent(key, action);
        }
        public void RegistEvent<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            if (action == null) return;
            RegistEvent(key, action);
        }
        public void RegistEvent<T1, T2, T3, T4>(int key, Action<T1, T2, T3, T4> action)
        {
            if (action == null) return;
            RegistEvent(key, action);
        }

        protected void RegistEvent(int key, MulticastDelegate observer)
        {
            if (m_observerMap.ContainsKey(key) && m_observerMap[key] == null)
            {
                m_observerMap[key] = new List<MulticastDelegate>();
            }
            else
            {
                m_observerMap.Add(key, new List<MulticastDelegate>() );
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
            RemoveEvent(key, action);
        }
        public void RemoveEvent<T>(int key, Action<T> action)
        {
            RemoveEvent(key, action);
        }
        public void RemoveEvent<T1, T2>(int key, Action<T1, T2> action)
        {
            RemoveEvent(key, action);
        }
        public void RemoveEvent<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            RemoveEvent(key,action);
        }
        public void RemoveEvent<T1, T2, T3, T4>(int key, Action<T1, T2, T3, T4> action)
        {
            RemoveEvent(key, action);
        }

        protected bool RemoveEvent(int key, MulticastDelegate action)
        {
            if (action == null) return false;

            if (m_observerMap.ContainsKey(key))
            {
                var list = m_observerMap[key];
                if (list.Contains(action))
                {
                    list.Remove(action);
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
                    var Action = list[i];
                    if (Action is Action)
                    {
                        TryRunAction(key, Action);
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
                    var Action = list[i];

                    if (Action is Action<T>)
                    {
                        TryRunAction(key, Action, value);
                    }
                    else if (Action is Action)
                    {
                        TryRunAction(key, Action);
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
                    var Action = list[i];

                    if (Action is Action<T1, T2>)
                    {
                        TryRunAction(key, Action, value1, value2);
                    }
                    else if (Action is Action<T1>)
                    {
                        TryRunAction(key, Action, value1);
                    }
                    else if (Action is Action)
                    {
                        TryRunAction(key, Action);
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
                    var Action = list[i];
                    if (Action is Action<T1, T2, T3>)
                    {
                        TryRunAction(key, Action, value1, value2, value3);
                    }
                    else if (Action is Action<T1, T2>)
                    {
                        TryRunAction(key, Action, value1, value2);
                    }
                    else if (Action is Action<T1>)
                    {
                        TryRunAction(key, Action, value1);
                    }
                    else if (Action is Action)
                    {
                        TryRunAction(key, Action);
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
                    var Action = list[i];
                    if (Action is Action<T1, T2, T3,T4>)
                    {
                        TryRunAction(key, Action, value1, value2, value3,value4);
                    }
                    else if (Action is Action<T1, T2, T3>)
                    {
                        TryRunAction(key, Action, value1, value2,value3);
                    }
                    else if (Action is Action<T1, T2>)
                    {
                        TryRunAction(key, Action, value1,value2);
                    }
                    else if (Action is Action<T1>)
                    {
                        TryRunAction(key, Action, value1);
                    }
                    else if (Action is Action)
                    {
                        TryRunAction(key, Action);
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
        protected void TryRunAction(int eventKey, object Action)
        {
            isRuning = true;
            try
            {
                (Action as Action).Invoke();
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }

        protected void TryRunAction<T>(int eventKey, object Action,T value)
        {
            isRuning = true;
            try
            {
                (Action as Action<T>).Invoke(value);
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }

        protected void TryRunAction<T1, T2>(int eventKey, object Action, T1 value1, T2 value2)
        {
            isRuning = true;
            try
            {
                (Action as Action<T1, T2>).Invoke(value1, value2);
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }
        protected void TryRunAction<T1, T2, T3>(int eventKey, object Action, T1 value1, T2 value2, T3 value3)
        {
            isRuning = true;
            try
            {
                (Action as Action<T1, T2, T3>).Invoke(value1, value2, value3);
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }
        protected void TryRunAction<T1,T2,T3,T4>(int eventKey, object Action, T1 value1,T2 value2,T3 value3,T4 value4)
        {
            isRuning = true;
            try
            {
                (Action as Action<T1,T2,T3,T4>).Invoke(value1,value2,value3,value4);
            }
            catch (Exception e)
            {
                MessageException(eventKey, e);
            }
            isRuning = false;
        }

        protected void RunEndAction()
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
