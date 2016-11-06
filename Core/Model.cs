using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Reflection;
public class Model : IModel
{
	public static volatile IModel Instance = new Model();

    readonly object m_syncRoot = new object();
    IDictionary<string, IProxy> m_proxyMap;
    Dictionary<string, UnityAction<IProxy>> waitRegisterEvents = new Dictionary<string, UnityAction<IProxy>>();
	private Model()
	{
		m_proxyMap = new Dictionary<string, IProxy>();
	}
   
	public void RegisterProxy(IProxy proxy)
	{
		lock (m_syncRoot){
            m_proxyMap[proxy.ProxyName] = proxy;
		}

        if (waitRegisterEvents.ContainsKey(proxy.ProxyName))
        {
            waitRegisterEvents[proxy.ProxyName].Invoke(proxy);
            waitRegisterEvents.Remove(proxy.ProxyName);
        }
    }

    public void RetrieveProxy<T>(string proxyName,UnityAction<T> retrieved) where T:IProxy
    {
        if (retrieved == null) return; 

        if (HasProxy(proxyName))
        {
            retrieved(RetrieveProxy<T>(proxyName));
        }
        else
        {
            waitRegisterEvents.Add(proxyName,(x)=> { retrieved((T)x); });
        }
    }

    T RetrieveProxy<T>(string proxyName) 
	{
		lock (m_syncRoot)
		{
			if (!m_proxyMap.ContainsKey(proxyName)) return default(T);
            return (T)m_proxyMap[proxyName];
		}
	}
	public bool HasProxy(string proxyName)
	{
		lock (m_syncRoot)
		{
			return m_proxyMap.ContainsKey(proxyName);
		}
	}
	public IProxy RemoveProxy(string proxyName)
	{
		IProxy proxy = null;

		lock (m_syncRoot)
		{
			if (m_proxyMap.ContainsKey(proxyName))
			{
				proxy = RetrieveProxy<IProxy>(proxyName);
				m_proxyMap.Remove(proxyName);
			}
		}
        
        return proxy;
	}

    public void CansaleRetrieve(string proxyName)
    {
        if (waitRegisterEvents.ContainsKey(proxyName))
        {
            waitRegisterEvents.Remove(proxyName);
        }
    }
}