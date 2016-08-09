using UnityEngine;
using System.Collections;

public interface IModel {
    void RegisterProxy(IProxy type);
    T RetrieveProxy<T>(string proxyName) where T :IProxy;
    IProxy RetrieveProxy(string proxyName);
    IProxy RemoveProxy(string proxyName);
    bool HasProxy(string proxyName);
}
