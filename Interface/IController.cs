using System;

public interface IController {
	void RegisterCommand(ObserverName commandName,ICommand command);
	ICommand RemoveCommand(ObserverName commandName);
	bool HasCommand(ObserverName commandName);
}
