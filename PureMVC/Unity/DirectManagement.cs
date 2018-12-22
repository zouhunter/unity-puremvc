using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace PureMVC.Unity
{
    public abstract class DirectManagement<GameManager> : Facade where GameManager : DirectManagement<GameManager>, new()
    {
        protected static GameManager instance = default(GameManager);
        private static object lockHelper = new object();
        private static bool isQuit = false;
        public bool connected { get; protected set; }
        private EventDispatcher _eventDispatch;
        public EventDispatcher eventDispatch { get { return _eventDispatch; } }
        protected Program<GameManager> _program;
        public Program<GameManager> program { get { return _program; } }
        public static GameManager Instence
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null && !isQuit)
                        {
                            instance = new GameManager();
                            instance.notifyNotHandle = instance.OnNotifyNotHandle;
                            instance._eventDispatch = new EventDispatcher();
                            instance._eventDispatch.messageNoHandle = instance.OnEventNotHandled;
                            instance._eventDispatch.messageExceptionHandle = instance.OnEventExecuteException;
                            instance.OnFrameWorkLunched();
                        }
                    }
                }
                return instance;
            }
        }
        internal void RegistProgram(Program<GameManager> program)
        {
            if (program != null)
            {
                connected = true;
                this._program = program;
            }
        }
        internal void RemoveProgram(Program<GameManager> program)
        {
            if (program == this.program)
            {
                connected = false;
                this._program = null;
            }
        }
        protected virtual void OnEventExecuteException(int eventID, Exception exception)
        {
            Debug.LogWarningFormat("【Exception Event】 ID: {0} Detail: {1}", eventID, exception.Message);
        }
        protected virtual void OnEventNotHandled(int eventID, string err)
        {
            Debug.LogWarningFormat("【Unhandled Event】 ID: {0} Detail: {1}", eventID, err);
        }
        protected virtual void OnNotifyNotHandle(int observerID)
        {
            Debug.LogWarning("【Unhandled Notify】 ID: " + observerID);
        }
        protected abstract void OnFrameWorkLunched();
        protected abstract void OnApplicationQuit();
        internal void ApplicationQuit()
        {
            isQuit = true;
            OnApplicationQuit();
        }
    }
}