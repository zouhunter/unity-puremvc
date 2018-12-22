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
                this._program = program;
            }
        }
        internal void RemoveProgram(Program<GameManager> program)
        {
            if (program == this.program)
            {
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
        protected static void AssignmentPropertiesID<Tp>()
        {
            var properties = typeof(Tp).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);

            for (int i = 0; i < properties.Length; i++)
            {
                properties[i].SetValue(null, i, null);
            }
        }
        protected static void AssignmentFieldsID<Tp>()
        {
            var fields = typeof(Tp).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);

            for (int i = 0; i < fields.Length; i++)
            {
                fields[i].SetValue(null, i, null);
            }
        }
        internal void OnApplicationQuit()
        {
            isQuit = true;
        }
    }
}