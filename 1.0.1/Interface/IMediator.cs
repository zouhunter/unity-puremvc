using UnityEngine;
using System.Collections.Generic;

public interface IMediator {
    string MediatorName { get;}
    Component ViewComponent{ get; set;}
    IList<NotiConst> ListNotificationInterests();
    void HandleNotification(INotification notify);
    void OnRegister();
    void OnRemove();
}
