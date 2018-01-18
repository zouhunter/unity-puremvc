
using System.Collections.Generic;

namespace PureMVC
{
    public interface IProxy<T> : IAcceptor
    {
        T Data { get; set; }
    }
}