using System;

namespace PureMVC
{

    public interface IController
    {
        void RegisterCommand<T, P>(string observeName) where T : ICommand<P>, new();
        void RegisterCommand<T>(string observeName) where T : ICommand, new();
        void RemoveCommand(string commandName);
        bool HasCommand(string commandName);
    }
}