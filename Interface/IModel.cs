using System;
using UnityEngine.Events;
namespace UnityEngine
{

    public interface IModel
    {
        void RegisterProxy(IProxy type);
        void CansaleRetrieve(string proxyName);
        void RetrieveProxy<T>(string proxyName, UnityAction<T> retrieved);
        IProxy RemoveProxy(string proxyName);
        bool HasProxy(string proxyName);
    }
}