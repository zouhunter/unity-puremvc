using UnityEngine;
using System.Collections;

public class EventTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SceneMain.Current.RegisterEvent<string>(EventKey.FirstEvent, Print);
    }

    void Print(string arg0)
    {
        SceneMain.Current.RemoveEvent<string>(EventKey.FirstEvent, Print);
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = arg0;
    }
}
