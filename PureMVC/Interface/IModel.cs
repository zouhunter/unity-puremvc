using System;

namespace PureMVC
{
    public interface IModel
    {
        void RegisterProxy<T>(IProxy<T> type);
        void CansaleRetrieve(int proxyKey);
        T RetrieveData<T>(int proxyKey);
        void RetrieveData<T>(int proxyKey, Action<T> retrieved);
        IProxy<T> RetrieveProxy<T>(int proxyKey);
        void RetrieveProxy<T>(int proxyKey, Action<IProxy<T>> retrieved);
        P RetrieveProxy<P, T>(int proxyKey) where P : IProxy<T>;
        void RetrieveProxy<P, T>(int proxyKey, Action<P> retrieved) where P : IProxy<T>;
        void RemoveProxy(int proxyKey);
        bool HasProxy(int proxyKey);
    }
}