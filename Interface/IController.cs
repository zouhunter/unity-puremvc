using System;
namespace UnityEngine
{

    public interface IController
    {
        void RegisterCommand<T, P>() where T : IAcceptor, new();
        void RegisterCommand<T>()  where T : IAcceptor, new();
        void RemoveCommand(string commandName);
        bool HasCommand(string commandName);
    }
}