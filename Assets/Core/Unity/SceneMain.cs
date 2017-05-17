using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
namespace UnityEngine
{

    public class SceneMain : MonoBehaviour
    {
        public class EventHold
        {
            public UnityEngine.Events.UnityAction<string> MessageNotHandled;
            public Dictionary<string, UnityAction<object>> m_needHandle = new Dictionary<string, UnityAction<object>>();
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
            public void AddDelegate(string key, UnityAction<object> handle)
            {
                // First check if we know about the message type
                if (!m_needHandle.ContainsKey(key))
                {
                    m_needHandle.Add(key, handle);
                }
                else
                {
                    m_needHandle[key] += handle;
                }
            }
            public bool RemoveDelegate(string key, UnityAction<object> handle)
            {
                if (m_needHandle.ContainsKey(key))
                {
                    m_needHandle[key] -=  handle;
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
                    m_needHandle[key].Invoke(null);

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
                    m_needHandle[key].Invoke(value);

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

        protected static SceneMain _abstruct;

        public static SceneMain Current
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

        public void RegisterEvent<T>(string noti, UnityAction<object> even)
        {
            _eventHold.AddDelegate(noti, even);
        }


        public void RemoveEvent<T>(string noti, UnityAction<object> even)
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

    public class SceneMain<T> : SceneMain where T: SceneMain
    {
        public new static T Current { get { return (T)_abstruct; } }

        protected override void Awake()
        {
            base.Awake();
        }
    }

}