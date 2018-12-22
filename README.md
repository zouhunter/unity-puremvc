# unity-puremvc
在Unity中使用PureMVC
```
/// <summary>
/// 跨场景全局管理
/// </summary>
public class GameManager : DirectManagement<GameManager>
{
    protected override void OnFrameWorkLunched()
    {
        
    }
}

/// <summary>
/// 场景运行管理
/// </summary>
using PureMVC.Unity;
public class Main : Program<GameManager>{

}
```
