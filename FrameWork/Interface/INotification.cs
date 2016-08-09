using System;

public interface INotification
{
	ObserverName ObserverName { get; set; }
    Type Type { get; set; }
    string ToString { get; }
}

public interface INotification<T>:INotification{
    T Body { get; set; }
}
