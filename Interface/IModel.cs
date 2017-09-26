﻿using System;
using UnityEngine.Events;
namespace UnityEngine
{

    public interface IModel
    {
        void RegisterProxy<T>(IProxy<T> type);
        void CansaleRetrieve(string proxyName);
        void RetrieveData<T>(string proxyName, UnityAction<T> retrieved);
        void RetrieveProxy<T>(string proxyName, UnityAction<IProxy<T>> retrieved);
        IProxy<T> RemoveProxy<T>(string proxyName);
        bool HasProxy(string proxyName);
    }
}