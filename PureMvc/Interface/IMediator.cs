using UnityEngine;
using System.Collections.Generic;
public interface IMediator
{
    IList<string> ListNotificationInterests();
}
public interface ISimpleMediator: IMediator
{
    void HandleNotification(INotification notify);
}
public interface IMediator<T>:IMediator
{
    void HandleNotification(INotification<T> notify);
}
