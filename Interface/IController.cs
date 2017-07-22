using System;
namespace UnityEngine
{

    public interface IController
    {
        void RegisterCommand<T>(Type type);
        void RegisterCommand(Type type);
        void RemoveCommand(string commandName);
        bool HasCommand(string commandName);
    }
}