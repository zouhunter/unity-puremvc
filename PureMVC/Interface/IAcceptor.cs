using System.Collections.Generic;

namespace PureMVC
{
    public interface IAcceptor
    {
        int Acceptor { get; }
    }
    public interface IAcceptors 
    {
        IList<int> Acceptors { get; }
    }
}