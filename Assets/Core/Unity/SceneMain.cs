using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
namespace PureMVC.Unity
{

    public class SceneMain : MonoBehaviour
    {
        public class EventHold
        {
            public System.Action<string> MessageNotHandled;
            public Dictionary<string, Delegate> m_needHandle = new Dictionary<string, Delegate>();
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
            public void AddDelegate(string key, Delegate handle)
            {
                // First check if we know about the message type
                if (!m_needHandle.ContainsKey(key))
                {
                    m_needHandle.Add(key, handle);
                }
                else
                {
                    m_needHandle[key] = Delegate.Combine(m_needHandle[key], handle);
                }
            }
            public bool RemoveDelegate(string key, Delegate handle)
            {
                if (m_needHandle.ContainsKey(key))
                {
                    m_needHandle[key] = Delegate.Remove(m_needHandle[key], handle);
                    if (m_needHandle[key] == null)
                    {
                        m_needHandle.Remove(key);
                        return false;
                    }
                }
                return true;
            }
            public void RemoveDelegates(string key)
            {
                if (m_needHandle.ContainsKey(key))
                {
                    m_needHandle.Remove(key);
                }
            }
            public bool HaveEvent(string key)
            {
                return m_needHandle.ContainsKey(key);
            }
            #endregion

            #region 触发事件
            public void NotifyObserver(string key)
            {
                bool lReportMissingRecipient = true;

                if (m_needHandle.ContainsKey(key))
                {
                    m_needHandle[key].DynamicInvoke();

                    lReportMissingRecipient = false;
                }

                // If we were unable to send the message, we may need to report it
                if (lReportMissingRecipient)
                {
                    NoMessageHandle(key);
                }
            }
            public void NotifyObserver<T>(string key, T value)
            {
                bool lReportMissingRecipient = true;

                if (m_needHandle.ContainsKey(key))
                {
                    m_needHandle[key].DynamicInvoke(value);

                    lReportMissingRecipient = false;
                }

                // If we were unable to send the message, we may need to report it
                if (lReportMissingRecipient)
                {
                    NoMessageHandle(key);
                }
            }
            #endregion
        }

        private static SceneMain _abstruct;
        public static SceneMain Abstruct
        {
            get
            {
                return _abstruct;
            }
        }
        private EventHold _eventHold;
        protected virtual void Awake()
        {
            _eventHold = new EventHold();
            _abstruct = this;
        }
        #region 访问事件系统
        public void RegisterEvent(string noti, UnityAction even)
        {
            _eventHold.AddDelegate(noti, even);
        }

        public void RegisterEvent<T>(string noti, UnityAction<T> even)
        {
            _eventHold.AddDelegate(noti, even);
        }

        public void RemoveEvent(string noti, UnityAction even)
        {
            _eventHold.RemoveDelegate(noti, even);
        }

        public void RemoveEvent<T>(string noti, UnityAction<T> even)
        {
            _eventHold.RemoveDelegate(noti, even);
        }

        public void RemoveEvents(string noti)
        {
            _eventHold.RemoveDelegates(noti);
        }

        public void InvokeEvents(string noti)
        {
            _eventHold.NotifyObserver(noti);
        }
        public void InvokeEvents<T>(string noti, T data)
        {
            _eventHold.NotifyObserver(noti, data);
        }
        #endregion
    }

    public class SceneMain<T> : SceneMain
    {
        private static T _current;
        public static T Current { get { return _current; } }

        protected override void Awake()
        {
            base.Awake();
            _current = GetComponent<T>();
        }
    }

}