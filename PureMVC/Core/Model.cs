
using System;
using System.Collections.Generic;
namespace PureMVC
{
    public class Model : IModel
    {
        protected readonly object m_syncRoot = new object();
        protected IDictionary<int, IAcceptor> m_proxyMap;
        protected Dictionary<int, Action<IAcceptor>> waitRegisterEvents = new Dictionary<int, Action<IAcceptor>>();

        public Model()
        {
            m_proxyMap = new Dictionary<int, IAcceptor>();
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
        public void RetrieveData<T>(int proxyKey, Action<T> retrieved)
        {
            if (retrieved == null) return;

            if (HasProxy(proxyKey))
            {
                retrieved(RetrieveData<T>(proxyKey));
            }
            else
            {
                if (waitRegisterEvents.ContainsKey(proxyKey))
                {
                    waitRegisterEvents[proxyKey] += (x) =>
                    {
                        if (x is IProxy<T>)
                        {
                            retrieved((x as IProxy<T>).Data);
                        }
                    };
                }
                else
                {
                    waitRegisterEvents.Add(proxyKey, (x) =>
                    {
                        if (x is IProxy<T>)
                        {
                            retrieved((x as IProxy<T>).Data);
                        }
                    });
                }
            }
        }
        public void RetrieveProxy<T>(int proxyKey, Action<IProxy<T>> retrieved)
        {
            if (retrieved == null) return;

            if (HasProxy(proxyKey))
            {
                var proxy = RetrieveProxy<T>(proxyKey);
                if (proxy is IProxy<T>)
                {
                    retrieved((IProxy<T>)proxy);
                }
            }
            else
            {
                if (waitRegisterEvents.ContainsKey(proxyKey))
                {
                    waitRegisterEvents[proxyKey] += (x) =>
                    {
                        if (x is IProxy<T>)
                        {
                            retrieved(((IProxy<T>)x));
                        }
                    };
                }
                else
                {
                    waitRegisterEvents.Add(proxyKey, (x) =>
                    {
                        if (x is IProxy<T>)
                        {
                            retrieved(((IProxy<T>)x));
                        }
                    });
                }
            }
        }

        public void RetrieveProxy<P, T>(int proxyKey, Action<P> retrieved) where P : IProxy<T>
        {
            if (retrieved == null) return;

            if (HasProxy(proxyKey))
            {
                retrieved(RetrieveProxy<P, T>(proxyKey));
            }
            else
            {
                if (waitRegisterEvents.ContainsKey(proxyKey))
                {
                    waitRegisterEvents[proxyKey] += (x) =>
                    {
                        if (x is P)
                        {
                            retrieved(((P)x));
                        }
                    };
                }
                else
                {
                    waitRegisterEvents.Add(proxyKey, (x) =>
                    {
                        if (x is P)
                        {
                            retrieved(((P)x));
                        }
                    });
                }
            }
        }

        public P RetrieveProxy<P, T>(int proxyKey) where P : IProxy<T>
        {
            lock (m_syncRoot)
            {
                var proxy = RetrieveProxy<T>(proxyKey);
                if (proxy is P)
                {
                    return (P)proxy;
                }
                return default(P);
            }
        }

        public T RetrieveData<T>(int proxyKey)
        {
            lock (m_syncRoot)
            {
                if (m_proxyMap.ContainsKey(proxyKey) && m_proxyMap[proxyKey] is IProxy<T>)
                {
                    return ((m_proxyMap[proxyKey] as IProxy<T>).Data);
                }
                else
                {
                    return default(T);
                }
            }
        }
        public IProxy<T> RetrieveProxy<T>(int proxyKey)
        {
            lock (m_syncRoot)
            {
                if (m_proxyMap.ContainsKey(proxyKey) && m_proxyMap[proxyKey] is IProxy<T>)
                {
                    return ((m_proxyMap[proxyKey] as IProxy<T>));
                }
                else
                {
                    return default(IProxy<T>);
                }
            }
        }
        public bool HasProxy(int proxyKey)
        {
            lock (m_syncRoot)
            {
                return m_proxyMap.ContainsKey(proxyKey);
            }
        }
        public void RemoveProxy(int proxyKey)
        {
            lock (m_syncRoot)
            {
                if (m_proxyMap.ContainsKey(proxyKey))
                {
                    m_proxyMap.Remove(proxyKey);
                }
            }
        }

        public void CansaleRetrieve(int proxyKey)
        {
            if (waitRegisterEvents.ContainsKey(proxyKey))
            {
                waitRegisterEvents.Remove(proxyKey);
            }
        }
    }
}