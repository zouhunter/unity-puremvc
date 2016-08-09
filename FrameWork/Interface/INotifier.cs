using UnityEngine;
using System;

public interface INotifier {
    void SendNotification(NotiConst notificationName);
    void SendNotification(NotiConst notificationName, object body);
    void SendNotification(NotiConst notificationName, object body, Type type);
}
