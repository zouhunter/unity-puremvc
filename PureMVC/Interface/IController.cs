using System;

namespace PureMVC
{

    public interface IController
    {
        void RegisterCommand<T, P>(int observeName) where T : ICommand<P>, new();
        void RegisterCommand<T>(int observeName) where T : ICommand, new();
        void RemoveCommand(int observeName);
        bool HasCommand(int observeName);
    }
}