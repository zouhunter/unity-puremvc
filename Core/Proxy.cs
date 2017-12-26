using System;



    public class Proxy<T> : IProxy<T>
    {
        public Proxy(string name)
        {
            m_proxyName = name;
        }
        public Proxy(string name, T data)
        {
            m_proxyName = name;
            Data = data;
        }

        public virtual T Data { get; set; }

        protected string m_proxyName;
        public string Acceptor
        {
            get
            {
                return m_proxyName;
            }
        }
    }
