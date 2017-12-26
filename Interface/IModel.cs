using System;

public interface IModel
{
    void RegisterProxy<T>(IProxy<T> type);
    void CansaleRetrieve(string proxyName);
    void RetrieveData<T>(string proxyName, Action<T> retrieved);
    void RetrieveProxy<T>(string proxyName, Action<IProxy<T>> retrieved);
    void RetrieveProxy<P, T>(string proxyName, Action<P> retrieved) where P : IProxy<T>;
    void RemoveProxy(string proxyName);
    bool HasProxy(string proxyName);
}
