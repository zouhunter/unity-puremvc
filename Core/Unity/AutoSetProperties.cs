
using System.Collections;
using System.Reflection;
using System;


public partial class ProxyName
{
    static ProxyName()
    {
        InitProperties<ProxyName>();
    }
    public static void InitProperties<T>()
    {
        var fields = typeof(T).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
        foreach (var item in fields)
        {
            item.SetValue(null, item.Name, null);
        }
    }
}
public partial class EventKey
{
    static EventKey()
    {
        InitProperties<EventKey>();
    }
    public static void InitProperties<T>()
    {
        var fields = typeof(T).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
        foreach (var item in fields)
        {
            item.SetValue(null, item.Name, null);
        }
    }
}
public partial class ObserverName
{
    static ObserverName()
    {
        InitProperties<ObserverName>();
    }
    public static void InitProperties<T>()
    {
        var fields = typeof(T).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
        foreach (var item in fields)
        {
            item.SetValue(null, item.Name, null);
        }
    }
}