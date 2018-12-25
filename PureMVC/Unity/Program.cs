
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PureMVC.Unity
{
    public abstract class Program<GameManager> : MonoBehaviour where GameManager : DirectManagement<GameManager>, new()
    {
        protected GameManager directManager;
        public delegate void ProgramBoolCallback(bool status);
        public delegate void ProgramVoidCallback();
        public delegate void ExceptionCallBack(Exception e);
        public event ProgramBoolCallback onApplicationPause;
        public event ProgramBoolCallback onApplicationFocus;
        public event ProgramVoidCallback onApplicationQuit;
        public event ProgramVoidCallback onApplicationUpdate;
        public event ProgramVoidCallback onApplicationFixedUpdate;
        public event ProgramVoidCallback onApplicationLateUpdate;
        public event ExceptionCallBack onEventExcption;

        protected virtual void Awake()
        {
            directManager = DirectManagement<GameManager>.Instence;
            directManager.RegistProgram(this);
        }
        protected virtual void OnDestroy()
        {
            directManager.RemoveProgram(this);
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

    }

}