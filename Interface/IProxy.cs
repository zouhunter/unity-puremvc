using UnityEngine;
using System.Collections.Generic;
namespace UnityEngine
{
    public interface IProxy<T> : IAcceptor
    {
        T Data { get; set; }
    }
}