using UnityEngine;
using System.Collections.Generic;
namespace UnityEngine
{

    public interface IProxy
    {
        string ProxyName { get; }
    }
    public interface IProxy<T> : IProxy
    {
        T Data { get; set; }
    }
}