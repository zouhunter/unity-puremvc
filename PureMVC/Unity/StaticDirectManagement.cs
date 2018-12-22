using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Reflection;

namespace PureMVC.Unity
{
    public abstract class StaticDirectManagement<GameManager> : DirectManagement<GameManager> where GameManager : StaticDirectManagement<GameManager>, new()
    {
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
        public static void Retrive_Proxy<T>(int proxyKey,Action<IProxy<T>> ontrive)
        {
            Instence.RetrieveProxy<T>(proxyKey, ontrive);
        }
        public static void Remove_Proxy(int proxyKey)
        {
            Instence.RemoveProxy(proxyKey);
        }

        #endregion

        #region Events
        public static void Regist(int eventKey, Action callBack)
        {
            Instence.eventDispatch.RegistEvent(eventKey, callBack);
        }
        public static void Regist<T>(int eventKey, Action<T> callBack)
        {
            Instence.eventDispatch.RegistEvent(eventKey, callBack);
        }
        public static void Regist<T1,T2>(int eventKey, Action<T1,T2> callBack)
        {
            Instence.eventDispatch.RegistEvent(eventKey, callBack);
        }
        public static void Regist<T1, T2,T3>(int eventKey, Action<T1, T2,T3> callBack)
        {
            Instence.eventDispatch.RegistEvent(eventKey, callBack);
        }
        public static void Regist<T1, T2, T3,T4>(int eventKey, Action<T1, T2, T3,T4> callBack)
        {
            Instence.eventDispatch.RegistEvent(eventKey, callBack);
        }
        public static void Remove_Events(int eventKey)
        {
            Instence.eventDispatch.RemoveEvents(eventKey);
        }
        public static void Remove_Event(int eventKey,Action callBack)
        {
            Instence.eventDispatch.RemoveEvent(eventKey, callBack);
        }
        public static void Remove_Event<T>(int eventKey, Action<T> callBack)
        {
            Instence.eventDispatch.RemoveEvent(eventKey, callBack);
        }
        public static void Remove_Event<T1,T2>(int eventKey, Action<T1,T2> callBack)
        {
            Instence.eventDispatch.RemoveEvent<T1,T2>(eventKey, callBack);
        }
        public static void Remove_Event<T1, T2,T3>(int eventKey, Action<T1, T2,T3> callBack)
        {
            Instence.eventDispatch.RemoveEvent<T1, T2,T3>(eventKey, callBack);
        }
        public static void Remove_Event<T1, T2, T3,T4>(int eventKey, Action<T1, T2, T3,T4> callBack)
        {
            Instence.eventDispatch.RemoveEvent<T1, T2, T3,T4>(eventKey, callBack);
        }
        public static void Execute(int eventKey)
        {
            Instence.eventDispatch.ExecuteEvents(eventKey);
        }
        public static void Execute<T1>(int eventKey, T1 value1)
        {
            Instence.eventDispatch.ExecuteEvents(eventKey, value1);
        }
        public static void Execute<T1, T2>(int eventKey, T1 value1, T2 value2)
        {
            Instence.eventDispatch.ExecuteEvents(eventKey, value1, value2);
        }
        public static void Execute<T1, T2, T3>(int eventKey, T1 value1, T2 value2, T3 value3)
        {
            Instence.eventDispatch.ExecuteEvents(eventKey, value1, value2, value3);
        }
        public static void Execute<T1,T2,T3,T4>(int eventKey,T1 value1,T2 value2,T3 value3,T4 value4 )
        {
            Instence.eventDispatch.ExecuteEvents(eventKey,value1,value2,value3,value4);
        }
        #endregion

        #region Notify
        public static void Notify(int observerKey)
        {
            Instence.SendNotification(observerKey);
        }
        public static void Notify<T>(int observerKey,T  argument)
        {
            Instence.SendNotification(observerKey,argument);
        }
        #endregion

        #region ArgumentHelper
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
        #endregion
    }
}
