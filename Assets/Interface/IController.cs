using System;
namespace UnityEngine
{

    public interface IController
    {
        void RegisterCommand<T>(string commandName, Type type);
        void RegisterCommand(string commandName, Type type);
        void RemoveCommand(string commandName);
        bool HasCommand(string commandName);
    }
}