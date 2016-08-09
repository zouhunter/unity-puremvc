using UnityEngine;
using System.Collections;

public interface IModel {
    void RegisterProxy(IProxy type);
    IProxy RetrieveProxy(string proxyName);
    IProxy RemoveProxy(string proxyName);
    bool HasProxy(string proxyName);
}
