using System;

public interface INotification
{
	ObserverName ObserverName { get; set; }
    Type Type { get; set; }
    string ToString { get; }
    bool isUsing { get; set; }
}

public interface INotification<T>:INotification{
    T Body { get; set; }
}
