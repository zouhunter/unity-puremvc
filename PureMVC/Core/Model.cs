
using System;
using System.Collections.Generic;
namespace PureMVC
{
    public class Model : IModel
    {
        protected readonly object m_syncRoot = new object();
        protected IDictionary<string, IAcceptor> m_proxyMap;
        protected Dictionary<string, Action<IAcceptor>> waitRegisterEvents = new Dictionary<string, Action<IAcceptor>>();

        internal Model()
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
        public void RetrieveData<T>(string proxyName, Action<T> retrieved)
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
        public void RetrieveProxy<T>(string proxyName, Action<IProxy<T>> retrieved)
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

        public void RetrieveProxy<P, T>(string proxyName, Action<P> retrieved) where P : IProxy<T>
        {
            if (retrieved == null) return;

            if (HasProxy(proxyName))
            {
                retrieved(RetrieveProxy<P, T>(proxyName));
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

        public P RetrieveProxy<P, T>(string proxyName) where P : IProxy<T>
        {
            lock (m_syncRoot)
            {
                var proxy = RetrieveProxy<T>(proxyName);
                if (proxy is P)
                {
                    return (P)proxy;
                }
                return default(P);
            }
        }

        public T RetrieveData<T>(string proxyName)
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
        public IProxy<T> RetrieveProxy<T>(string proxyName)
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
        public void RemoveProxy(string proxyName)
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