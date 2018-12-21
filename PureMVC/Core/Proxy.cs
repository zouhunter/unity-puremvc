using System;



namespace PureMVC
{
    public class Proxy<T> : IProxy<T>
    {
        public Proxy(int name)
        {
            m_proxyName = name;
        }
        public Proxy(int name, T data)
        {
            m_proxyName = name;
            Data = data;
        }

        public virtual T Data { get; set; }

        protected int m_proxyName;
        public int Acceptor
        {
            get
            {
                return m_proxyName;
            }
        }
    }
}