using System;
using UnityEngine.Events;

public interface IModel {
	void RegisterProxy(IProxy type);
	void CansaleRetrieve(string proxyName);
    void RetrieveProxy<T>(string proxyName, UnityAction<T> retrieved) where T : IProxy;
    IProxy RemoveProxy(string proxyName);
	bool HasProxy(string proxyName);
}
