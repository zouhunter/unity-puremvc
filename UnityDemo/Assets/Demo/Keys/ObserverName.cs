using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverName
{
    public static readonly int CHANGE_COLOR;//修改Color
    public static readonly int NAVI_MOVE;//navi移动
    public static readonly int RANDOM_SCALE;//随机大小变化

    public static readonly int CHANGE_COLOR_UI;//修改Color

    static ObserverName()
    {
        var fields = typeof(ObserverName).GetFields(
            System.Reflection.BindingFlags.Static|
            System.Reflection.BindingFlags.GetField|
            System.Reflection.BindingFlags.Public);
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].SetValue(null, i);
        }
    }
}
