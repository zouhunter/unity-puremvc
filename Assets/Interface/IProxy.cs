using UnityEngine;
using System.Collections.Generic;
namespace PureMVC.Internal
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