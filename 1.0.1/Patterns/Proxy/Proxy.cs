using System;

public class Proxy : Notifier, IProxy, INotifier
{
    public static string NAME = "Proxy";
    public Proxy() : this(NAME, null)
    {
    }
    public Proxy(string proxyName)
        : this(proxyName, null)
    {
    }
    public Proxy(string proxyName, object data)
    {
        m_proxyName = (proxyName != null) ? proxyName : NAME;
        if (data != null) m_data = data;
    }

    public virtual string ProxyName
    {
        get { return m_proxyName; }
    }
    public virtual object Data
    {
        get { return m_data; }
        set { m_data = value; }
    }
    protected string m_proxyName;
    protected object m_data;
    public virtual void OnRegister()
    {
    }
    public virtual void OnRemove()
    {
    }
}