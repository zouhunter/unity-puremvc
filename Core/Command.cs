/// <summary>
/// 只应当在注册时被引用
/// </summary>
public abstract class Command : ICommand
{
    public abstract void Execute();
}
/// <summary>
/// 只应当在注册时被引用
/// </summary>
public abstract class Command<T> : ICommand<T>
{
    public abstract void Execute(T notification);
}
