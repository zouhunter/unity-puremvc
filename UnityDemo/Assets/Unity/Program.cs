
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PureMVC.Unity
{
    public abstract class Program<GameManager> : MonoBehaviour where GameManager : Management<GameManager>, new()
    {
        protected GameManager directManager;
        public event Action onApplicationEnable;
        public event Action onApplicationStart;
        public event Action<bool> onApplicationFocus;
        public event Action onApplicationUpdate;
        public event Action onApplicationFixedUpdate;
        public event Action onApplicationLateUpdate;
        public event Action onApplicationOnDrawGizmos;
        public event Action onApplicationOnGUI;
        public event Action<bool> onApplicationPause;
        public event Action onApplicationDisable;
        public event Action onApplicationDestory;
        public event Action onApplicationQuit;
        public event Action<Exception> onEventExcption;

        protected virtual void Awake()
        {
            directManager = Management<GameManager>.Instence;
            directManager.RegistProgram(this);
        }
        protected virtual void OnEnable()
        {
            if (onApplicationEnable != null)
            {
                try
                {
                    onApplicationEnable();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }
        protected virtual void Start()
        {
            if (onApplicationStart != null)
            {
                try
                {
                    onApplicationStart();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }

        protected virtual void Update()
        {
            if (onApplicationUpdate != null)
            {
                try
                {
                    onApplicationUpdate();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }

        protected virtual void LateUpdate()
        {
            if (onApplicationLateUpdate != null)
            {
                try
                {
                    onApplicationLateUpdate();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            if (onApplicationFixedUpdate != null)
            {
                try
                {
                    onApplicationFixedUpdate();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }
        protected virtual void OnDrawGizmos()
        {
            if (onApplicationOnDrawGizmos != null)
            {
                try
                {
                    onApplicationOnDrawGizmos();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }
        protected virtual void OnGUI()
        {
            if (onApplicationOnGUI != null)
            {
                try
                {
                    onApplicationOnGUI();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }
        protected virtual void OnDisable()
        {
            if (onApplicationDisable != null)
            {
                try
                {
                    onApplicationDisable();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (onApplicationDestory != null)
            {
                try
                {
                    onApplicationDestory();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
            directManager.RemoveProgram(this);
        }


        protected virtual void OnApplicationQuit()
        {
            directManager.ApplicationQuit();

            if (onApplicationQuit != null)
            {
                try
                {
                    onApplicationQuit();
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }
        protected virtual void OnApplicationPause(bool pauseStatus)
        {
            if (onApplicationPause != null)
            {
                try
                {
                    onApplicationPause(pauseStatus);
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }

        protected virtual void OnApplicationFocus(bool focusStatus)
        {
            if (onApplicationFocus != null)
            {
                try
                {
                    onApplicationFocus(focusStatus);
                }
                catch (Exception e)
                {
                    OnEventExction(e);
                }
            }
        }

        protected void OnEventExction(Exception e)
        {
            if (onEventExcption != null)
            {
                onEventExcption.Invoke(e);
            }
            else
            {
                throw e;
            }
        }

    }

}