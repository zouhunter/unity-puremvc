using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// 整个程序的入口，并且做辅助管理
/// </summary>
public class GameManager : MonoBehaviour
{
    #region 多线程单例
    static bool isOn = true;
    private static GameManager m_Instance;
    protected static readonly object m_staticSyncRoot = new object();
    private GameManager(){ }
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_Instance == null && isOn)
                    {
                        GameObject go = new GameObject("GameManager");
                        m_Instance = go.AddComponent<GameManager>();
                    }
                }
            }
            return m_Instance;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);  //防止销毁自己
    }
    #endregion

}