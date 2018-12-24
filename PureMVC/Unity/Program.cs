
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PureMVC.Unity
{
    public abstract class Program<GameManager>: MonoBehaviour where GameManager: DirectManagement<GameManager>,new()
    {
        protected GameManager directManager;
        public delegate void ApplicationBoolCallback(bool status);
        public delegate void ApplicationVoidCallback();
        public ApplicationVoidCallback onApplicationQuit = null;
        public ApplicationBoolCallback onApplicationPause = null;
        public ApplicationBoolCallback onApplicationFocus = null;
        public ApplicationVoidCallback onApplicationUpdate = null;
        public ApplicationVoidCallback onApplicationFixedUpdate = null;
        public ApplicationVoidCallback onApplicationOnGUI = null;
        public ApplicationVoidCallback onApplicationOnDrawGizmos = null;
        public ApplicationVoidCallback onApplicationLateUpdate = null;
        protected virtual void Awake()
        {
            directManager =  DirectManagement<GameManager>.Instence;
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
                onApplicationUpdate();
            }
        }

        protected virtual void LateUpdate()
        {
            if (onApplicationLateUpdate != null)
            {
                onApplicationLateUpdate();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (onApplicationFixedUpdate != null)
            {
                onApplicationFixedUpdate();
            }
        }

        protected virtual void OnGUI()
        {
            if (onApplicationOnGUI != null)
                onApplicationOnGUI();
        }

        protected virtual void OnDrawGizmos()
        {
            if (onApplicationOnDrawGizmos != null)
                onApplicationOnDrawGizmos();
        }

        protected virtual void OnApplicationQuit()
        {
            directManager.ApplicationQuit();

            if (onApplicationQuit != null)
            {
                onApplicationQuit();
            }
        }

        /*
         * 强制暂停时，先 OnApplicationPause，后 OnApplicationFocus
         * 重新“启动”游戏时，先OnApplicationFocus，后 OnApplicationPause
         */
        protected virtual void OnApplicationPause(bool pauseStatus)
        {
            if (onApplicationPause != null)
            {
                onApplicationPause(pauseStatus);
            }
        }

        protected virtual void OnApplicationFocus(bool focusStatus)
        {
            if (onApplicationFocus != null)
            {
                onApplicationFocus(focusStatus);
            }
        }

    }

}