using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;

public class ColorProxy : Proxy<Color[]>
{
    public ColorProxy(int proxyName, params Color[] colors) : base(proxyName, colors) { }
}
