using System;

public interface IController {
	void RegisterCommand(string commandName,Type command);
	void ExecuteCommand(INotification notify);
    Type RemoveCommand(string commandName);
	bool HasCommand(string commandName);
}
