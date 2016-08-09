using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
/// <summary>
/// 将unity不支持的多线程通知加入到协程中
/// </summary>
public class AppFacade : Facade
{
    public static new IFacade Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance == null)
                        m_instance = new AppFacade();
                }
            }
            return m_instance;
        }
    }

    private Queue<INotification> waitToNoti = new Queue<INotification>();
    public AppFacade()
    {
        GameManager.Instance.StartCoroutine(WaitToDequeue());
    }
    /// <summary>
    /// 等待事件接收
    /// </summary>
    IEnumerator WaitToDequeue()
    {
        while(true)
        {
            yield return new WaitUntil(() => waitToNoti.Count>0);
            INotification noti = waitToNoti.Dequeue();
            Debug.Log("AppFacede Noti");
            base.NotifyObservers(noti);
        }
    }
    public override void NotifyObservers(INotification notification)
    {
        waitToNoti.Enqueue(notification);
    }
    public override void SendNotification(NotiConst notificationName)
    {
        NotifyObservers(new Notification(notificationName));
    }
    public override void SendNotification(NotiConst notificationName, object body)
    {
        NotifyObservers(new Notification(notificationName, body));
    }
    public override void SendNotification(NotiConst notificationName, object body, Type type)
    {
        NotifyObservers(new Notification(notificationName, body, type));
    }
    protected override void InitializeFacade()
    {
        base.InitializeFacade();
    }
}
