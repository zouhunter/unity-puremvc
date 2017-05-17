using UnityEngine;

public abstract class GameManager<T> : MonoBehaviour where T : GameManager<T>
{
    protected static T instance = default(T);
    private static object lockHelper = new object();
    private static bool isQuit = false;
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
                    }
                }
            }
            return instance;
        }
    }
    public static void StartGame()
    {
        if (instance == null)
        {
            Instence.LunchFrameWork();
            DontDestroyOnLoad(Instence.gameObject);
        }
    }
    protected virtual void OnApplicationQuit()
    {
        isQuit = true;
    }
    protected abstract void LunchFrameWork();
}