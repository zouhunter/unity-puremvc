using System;

using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public interface IAcceptor
{
    string Acceptor { get; }
}
public interface IAcceptors: IAcceptor
{
    IList<string> Acceptors { get; }
}
