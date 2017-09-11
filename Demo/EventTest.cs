using UnityEngine;
using System.Collections;

public class EventTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SceneMain.Current.RegisterEvent<string>("event", Print);
    }

    void Print(string arg0)
    {
        SceneMain.Current.RemoveEvent<string>("event", Print);
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = arg0;
    }
}
