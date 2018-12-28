using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace PureMVC.Unity
{
    public abstract class Management<GameManager> : Facade where GameManager : Management<GameManager>, new()
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


        #region Mediator
        public static void Regist(IMediator medator)
        {
            Instence.RegisterMediator(medator);
        }
        public static void Regist<T>(IMediator<T> mediator)
        {
            Instence.RegisterMediator(mediator);
        }
        public static void Remove(IMediator mediator)
        {
            Instence.RemoveMediator(mediator);
        }
        public static void Remove<T>(IMediator<T> mediator)
        {
            Instence.RemoveMediator(mediator);
        }
        #endregion

        #region Command
        public static void Regist<T>(int observerKey) where T : ICommand, new()
        {
            Instence.RegisterCommand<T>(observerKey);
        }
        public static void Regist<T, S>(int observerKey) where T : ICommand<S>, new()
        {
            Instence.RegisterCommand<T, S>(observerKey);
        }
        public static bool Have_Command(int observerKey)
        {
            return Instence.HaveCommand(observerKey);
        }
        public static void Remove_Command(int observeKey)
        {
            Instence.RemoveCommand(observeKey);
        }

        #endregion

        #region Proxy
        public static void Regist<T>(IProxy<T> proxy)
        {
            Instence.RegisterProxy<T>(proxy);
        }
        public static void Regist<T>(int proxyKey, T data)
        {
            Instence.RegisterProxy<T>(proxyKey, data);
        }
        public static bool Have_Proxy(int proxyKey)
        {
            return Instence.HaveProxy(proxyKey);
        }
        public static IProxy<T> Retrive_Proxy<T>(int proxyKey)
        {
            return Instence.RetrieveProxy<T>(proxyKey);
        }
        public static void Retrive_Proxy<T>(int proxyKey, Action<IProxy<T>> ontrive)
        {
            Instence.RetrieveProxy<T>(proxyKey, ontrive);
        }
        public static void Remove_Proxy(int proxyKey)
        {
            Instence.RemoveProxy(proxyKey);
        }

        #endregion

        #region Notify
        public static void Notify(int observerKey)
        {
            Instence.SendNotification(observerKey);
        }
        public static void Notify<T>(int observerKey, T argument)
        {
            Instence.SendNotification(observerKey, argument);
        }
        #endregion
    }
}