using UnityEngine;
using System.Collections.Generic;
public interface IProxy
{
    string ProxyName { get; }
}
public interface IProxy<T>:IProxy {
	T Data { get; set; }
}
