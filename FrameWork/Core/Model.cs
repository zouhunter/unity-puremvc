using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;
public class Model : IModel
{
    public static IModel Instance = new Model();
    protected readonly object m_syncRoot = new object();

    protected IDictionary<string, IProxy> m_proxyMap;
	protected Model()
	{
		m_proxyMap = new Dictionary<string, IProxy>();
		InitializeModel();
	}

	protected virtual void InitializeModel()
	{
		
	}

	public virtual void RegisterProxy(IProxy proxy)
	{
		lock (m_syncRoot)
		{
            m_proxyMap[proxy.ProxyName] = proxy;
		}

		proxy.OnRegister();
	}
	public virtual T RetrieveProxy<T>(string proxyName)
	{
		lock (m_syncRoot)
		{
			if (!m_proxyMap.ContainsKey(proxyName)) return default(T);
            return (T)m_proxyMap[proxyName];
		}
	}
	public virtual bool HasProxy(string proxyName)
	{
		lock (m_syncRoot)
		{
			return m_proxyMap.ContainsKey(proxyName);
		}
	}
	public virtual IProxy RemoveProxy(string proxyName)
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

		if (proxy != null) proxy.OnRemove();
        return proxy;
	}

}