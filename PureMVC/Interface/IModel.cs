using System;

namespace PureMVC
{
    public interface IModel
    {
        void RegisterProxy<T>(IProxy<T> type);
        void CansaleRetrieve(int proxyName);
        T RetrieveData<T>(int proxyName);
        void RetrieveData<T>(int proxyName, Action<T> retrieved);
        IProxy<T> RetrieveProxy<T>(int proxyName);
        void RetrieveProxy<T>(int proxyName, Action<IProxy<T>> retrieved);
        P RetrieveProxy<P, T>(int proxyName) where P : IProxy<T>;
        void RetrieveProxy<P, T>(int proxyName, Action<P> retrieved) where P : IProxy<T>;
        void RemoveProxy(int proxyName);
        bool HasProxy(int proxyName);
    }
}