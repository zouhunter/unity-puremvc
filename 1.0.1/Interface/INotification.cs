using UnityEngine;
using System;
/// <summary>
/// 通信非泛型，需要开箱操作
/// </summary>
public interface INotification {
    NotiConst ObserverName { get; set; }
    object Body { get; set; }
    Type Type { get; set; }
    string ToString { get; }
}
