using UnityEngine;
using System.Collections.Generic;
namespace PureMVC.Interfaces
{
    public interface IProxy
    {
        string ProxyName { get; }
        void OnRegister();
        void OnRemove();
    }
    public interface IProxy<T> : IProxy
    {
        T Data { get; set; }
    }
}