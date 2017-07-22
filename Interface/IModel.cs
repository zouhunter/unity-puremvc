using System;
using UnityEngine.Events;
namespace UnityEngine
{

    public interface IModel
    {
        void RegisterProxy<T>(IProxy<T> type);
        void CansaleRetrieve(string proxyName);
        void RetrieveProxy<T>(string proxyName, UnityAction<T> retrieved);
        IProxy<T> RemoveProxy<T>(string proxyName);
        bool HasProxy(string proxyName);
    }
}