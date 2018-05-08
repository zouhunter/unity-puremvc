using System;

namespace PureMVC
{
    public interface IModel
    {
        void RegisterProxy<T>(IProxy<T> type);
        void CansaleRetrieve(string proxyName);
        T RetrieveData<T>(string proxyName);
        void RetrieveData<T>(string proxyName, Action<T> retrieved);
        IProxy<T> RetrieveProxy<T>(string proxyName);
        void RetrieveProxy<T>(string proxyName, Action<IProxy<T>> retrieved);
        P RetrieveProxy<P, T>(string proxyName) where P : IProxy<T>;
        void RetrieveProxy<P, T>(string proxyName, Action<P> retrieved) where P : IProxy<T>;
        void RemoveProxy(string proxyName);
        bool HasProxy(string proxyName);
    }
}