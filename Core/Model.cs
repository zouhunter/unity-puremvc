using UnityEngine.Events;
using System.Collections.Generic;


namespace UnityEngine
{

    public class Model : IModel
    {
        public static volatile IModel Instance = new Model();

        readonly object m_syncRoot = new object();
        IDictionary<string, IAcceptor> m_proxyMap;
        Dictionary<string, UnityAction<IAcceptor>> waitRegisterEvents = new Dictionary<string, UnityAction<IAcceptor>>();
        private Model()
        {
            m_proxyMap = new Dictionary<string, IAcceptor>();
        }

        public void RegisterProxy<T>(IProxy<T> proxy)
        {
            lock (m_syncRoot)
            {
                m_proxyMap[proxy.Acceptor] = proxy;
            }

            if (waitRegisterEvents.ContainsKey(proxy.Acceptor))
            {
                waitRegisterEvents[proxy.Acceptor].Invoke(proxy);
                waitRegisterEvents.Remove(proxy.Acceptor);
            }
        }
        public void RetrieveData<T>(string proxyName, UnityAction<T> retrieved)
        {
            if (retrieved == null) return;

            if (HasProxy(proxyName))
            {
                retrieved(RetrieveData<T>(proxyName));
            }
            else
            {
                if (waitRegisterEvents.ContainsKey(proxyName))
                {
                    waitRegisterEvents[proxyName] += (x) =>
                    {
                        if (x is IProxy<T>)
                        {
                            retrieved((x as IProxy<T>).Data);
                        }
                    };
                }
                else
                {
                    waitRegisterEvents.Add(proxyName, (x) =>
                    {
                        if (x is IProxy<T>)
                        {
                            retrieved((x as IProxy<T>).Data);
                        }
                    });
                }
            }
        }
        public void RetrieveProxy<T>(string proxyName, UnityAction<IProxy<T>> retrieved) 
        {
            if (retrieved == null) return;

            if (HasProxy(proxyName))
            {
                var proxy = RetrieveProxy<T>(proxyName);
                if (proxy is IProxy<T>)
                {
                    retrieved((IProxy<T>)proxy);
                }
            }
            else
            {
                if (waitRegisterEvents.ContainsKey(proxyName))
                {
                    waitRegisterEvents[proxyName] += (x) =>
                    {
                        if (x is IProxy<T>)
                        {
                            retrieved(((IProxy<T>)x));
                        }
                    };
                }
                else
                {
                    waitRegisterEvents.Add(proxyName, (x) =>
                    {
                        if (x is IProxy<T>)
                        {
                            retrieved(((IProxy<T>)x));
                        }
                    });
                }
            }
        }

        public void RetrieveProxy<P, T>(string proxyName, UnityAction<P> retrieved) where P : IProxy<T>
        {
            if (retrieved == null) return;

            if (HasProxy(proxyName))
            {
                var proxy = RetrieveProxy<T>(proxyName);
                if (proxy is P)
                {
                    retrieved((P)proxy);
                }
            }
            else
            {
                if (waitRegisterEvents.ContainsKey(proxyName))
                {
                    waitRegisterEvents[proxyName] += (x) =>
                    {
                        if (x is P)
                        {
                            retrieved(((P)x));
                        }
                    };
                }
                else
                {
                    waitRegisterEvents.Add(proxyName, (x) =>
                    {
                        if (x is P)
                        {
                            retrieved(((P)x));
                        }
                    });
                }
            }
        }

        T RetrieveData<T>(string proxyName)
        {
            lock (m_syncRoot)
            {
                if (m_proxyMap.ContainsKey(proxyName) && m_proxyMap[proxyName] is IProxy<T>)
                {
                    return ((m_proxyMap[proxyName] as IProxy<T>).Data);
                }
                else
                {
                    return default(T);
                }
            }
        }
        IProxy<T> RetrieveProxy<T>(string proxyName)
        {
            lock (m_syncRoot)
            {
                if (m_proxyMap.ContainsKey(proxyName) && m_proxyMap[proxyName] is IProxy<T>)
                {
                    return ((m_proxyMap[proxyName] as IProxy<T>));
                }
                else
                {
                    return default(IProxy<T>);
                }
            }
        }
        public bool HasProxy(string proxyName)
        {
            lock (m_syncRoot)
            {
                return m_proxyMap.ContainsKey(proxyName);
            }
        }
        public void RemoveProxy<T>(string proxyName)
        {
            lock (m_syncRoot)
            {
                if (m_proxyMap.ContainsKey(proxyName))
                {
                    m_proxyMap.Remove(proxyName);
                }
            }
        }

        public void CansaleRetrieve(string proxyName)
        {
            if (waitRegisterEvents.ContainsKey(proxyName))
            {
                waitRegisterEvents.Remove(proxyName);
            }
        }
    }
}