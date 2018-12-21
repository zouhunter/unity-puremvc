# unity-puremvc
在Unity中使用PureMVC
```
/// <summary>
/// 跨场景全局管理
/// </summary>
public class GameManager : App<GameManager>
{
    protected override void OnFrameWorkLunched()
    {
        Debug.Log("OnFrameWorkLunched");
    }
}

/// <summary>
/// 场景运行管理
/// </summary>
public class Main : Program<GameManager> {
 
}

/// <summary>
/// 任何一个脚本中都可以直接访问：
/// 1.App<T>的实例(跨场景运行管理)
/// 2.Program实例（当前场景运行实例）
/// </summary>
public class AnyClass:MonoBehaviour
{
    public class TempCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("TempCommand");
            var data = GameManager.Instence.RetrieveData<TempData>("data");
            Debug.Log(data.value);
        }
    }
    public class TempMediator : Mediator
    {
        public TempMediator() : base("onkeyDown")
        {

        }

        public override void HandleNotification(string observerName)
        {
            Debug.Log("TempMediator");
        }
    }
    public class TempData
    {
        public string value = "数据包";
    }

    private void Start()
    {
        Program.Current.RegisterEvent<string>("onkeyDown", (key) => { Debug.Log("OnKeyDown:" + key); });
        GameManager.Instence.RegisterMediator(new TempMediator());
        GameManager.Instence.RegisterCommand<TempCommand>("onkeyDown");
        GameManager.Instence.RegisterProxy(new Proxy<TempData>("data", new TempData()));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Program.Current.Notify("onkeyDown", KeyCode.A.ToString());
            GameManager.Instence.SendNotification("onkeyDown");
        }
    }
}
```
