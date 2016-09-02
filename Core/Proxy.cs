using System;
public abstract class Proxy<T> :Notifyer, IProxy<T>
{
    private string m_proxyName;
    public Proxy() { m_proxyName = ToString(); }
    public Proxy(string name){m_proxyName = name;}
    public Proxy(string name, T data)
    {
        m_proxyName = name;
        Data = data;
    }

    public abstract T Data { get; set; }
    public virtual string ProxyName
    {
        get { return (m_proxyName != null) ? m_proxyName : this.ToString(); }
    }
    public virtual void OnRegister()
    {
    }
    public virtual void OnRemove()
    {
    }
}