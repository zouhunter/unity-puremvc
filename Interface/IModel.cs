using System;
using UnityEngine.Events;
namespace UnityEngine
{

    public interface IModel
    {
        void RegisterProxy(IAcceptor type);
        void CansaleRetrieve(string proxyName);
        void RetrieveProxy<T>(string proxyName, UnityAction<T> retrieved);
        IAcceptor RemoveProxy(string proxyName);
        bool HasProxy(string proxyName);
    }
}