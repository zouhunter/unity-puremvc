using UnityEngine;
using System.Collections.Generic;

    public interface IProxy<T> : IAcceptor
    {
        T Data { get; set; }
    }
