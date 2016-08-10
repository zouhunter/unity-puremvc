using System;
using PureMVC.Interfaces;

namespace PureMVC.Interfaces
{
    public interface IController
    {
        void RegisterCommand(string commandName, ICommand command);
        ICommand RemoveCommand(string commandName);
        bool HasCommand(string commandName);
    }
}
