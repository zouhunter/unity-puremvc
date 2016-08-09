using System;
public interface IController {
    void RegisterCommand(NotiConst noti,Type command);
    void ExecuteCommand(INotification notify);
    void RemoveCommand(NotiConst noti);
    bool HasCommand(ICommand noti);
}
