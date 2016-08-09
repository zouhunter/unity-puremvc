using System;
public abstract class Proxy : Notifier, IProxy, INotifier
{
    private string m_proxyName;
    public Proxy() { m_proxyName = ToString(); }
    public Proxy(string name)
    {
        m_proxyName = name;
    }
    public Proxy(string name,object data)
    {
        m_proxyName = name;
        Data = data;
    }
    public abstract object Data { get; set; }
    public virtual string ProxyName{
        get { return (m_proxyName != null) ? m_proxyName : this.ToString(); }
    }

    public virtual void OnRegister()
    {
    }
    public virtual void OnRemove()
    {
    }
}