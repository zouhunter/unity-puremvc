using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventName
{
    public static readonly int MOVE_PLAYER;//移动玩家

    static EventName()
    {
        var fields = typeof(EventName).GetFields(
         System.Reflection.BindingFlags.Static |
         System.Reflection.BindingFlags.GetField |
         System.Reflection.BindingFlags.Public);

        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].SetValue(null, i);
        }
    }
}
