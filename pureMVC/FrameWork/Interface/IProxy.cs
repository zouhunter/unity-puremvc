using UnityEngine;
using System.Collections.Generic;

public interface IProxy {
    string ProxyName { get; }
    object Data { get; set; }
    void OnRegister();
    void OnRemove();
}
