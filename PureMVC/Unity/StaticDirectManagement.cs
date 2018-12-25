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
