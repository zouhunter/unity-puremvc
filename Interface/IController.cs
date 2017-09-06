using System;
namespace UnityEngine
{

    public interface IController
    {
        void RegisterCommand<T, P>() where T : ICommand<P>, new();
        void RegisterCommand<T>() where T : ICommand, new();
        void RemoveCommand(string commandName);
        bool HasCommand(string commandName);
    }
}