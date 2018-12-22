using System;

namespace PureMVC
{

    public interface IController
    {
        void RegisterCommand<T, P>(int observeKey) where T : ICommand<P>, new();
        void RegisterCommand<T>(int observeKey) where T : ICommand, new();
        void RemoveCommand(int observeKey);
        bool HaveCommand(int observeKey);
    }
}