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
                            instance.OnFrameWorkLunched();
                        }
                    }
                }
                return instance;
            }
        }
        internal void RegistProgram(Program<GameManager> program)
        {
            if (this.program != null)
            {
                OnProgramRemoved(this.program);
                this._program = null;
                connected = false;
            }

            if (program != null)
            {
                connected = true;
                this._program = program;
                OnProgramRegisted(program);
            }
        }
        internal void RemoveProgram(Program<GameManager> program)
        {
            if (program != null && program == this.program)
            {
                OnProgramRemoved(program);
                this._program = null;
                connected = false;
            }
        }
        protected virtual void OnNotifyNotHandle(int observerID)
        {
            Debug.LogWarning("【Unhandled Notify】 ID: " + observerID);
        }
        protected abstract void OnFrameWorkLunched();
        protected abstract void OnProgramRegisted(Program<GameManager> program);
        protected abstract void OnProgramRemoved(Program<GameManager> program);
        internal void ApplicationQuit()
        {
            isQuit = true;
        }
    }
}