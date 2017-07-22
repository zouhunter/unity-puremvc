using UnityEngine;

public abstract class GameManager<T> : MonoBehaviour where T : GameManager<T>
{
    protected static T instance = default(T);
    private static object lockHelper = new object();
    private static bool isQuit = false;
    private static bool isOn = false;
    public static T Instence
    {
        get
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null && !isQuit)
                    {
                        GameObject go = new GameObject(typeof(T).ToString());
                        instance = go.AddComponent<T>();
                        Instence.LunchFrameWork();
                        DontDestroyOnLoad(go);
                    }
                }
            }
            return instance;
        }
    }
    public static void StartGame()
    {
        if(!isOn){
            instance = Instence;
            isOn = true;
        }
    }
    protected virtual void OnApplicationQuit()
    {
        isOn = false;
        isQuit = true;
    }
    protected abstract void LunchFrameWork();
}