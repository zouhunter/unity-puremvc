using UnityEngine;
using System.Collections.Generic;

public interface IMediator {
    string MediatorName { get;}
    IList<NotiConst> ListNotificationInterests();
    void HandleNotification(INotification notify);
    void OnRegister();
    void OnRemove();
}
