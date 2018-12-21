using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyName {
    public static readonly int COLOR_BLOCK_GIRL;//修改Color
    public static readonly int COLOR_BLOCK_BOY;//修改Color

    static ProxyName()
    {
        var fields = typeof(ProxyName).GetFields(
         System.Reflection.BindingFlags.Static |
         System.Reflection.BindingFlags.GetField |
         System.Reflection.BindingFlags.Public);

        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].SetValue(null, i);
        }
    }
}
