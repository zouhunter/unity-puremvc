using UnityEngine;
using System.IO;
using System.Text;

namespace PureMVC.Unity
{
    public class OutLog
    {
        private bool logExport;
        private bool isOverWrite;
        private bool isInit = false;
        private StringBuilder mWriteTxt;
        private string mOutPath;

        public void Initialize(bool logExport,bool isOverWrite)
        {
            this.logExport = logExport;
            this.isOverWrite = isOverWrite;
            if (!isInit)
            {
                mWriteTxt = new StringBuilder();
                Reset();
                isInit = true;
            }
        }

        private void Reset()
        {
            if (!logExport) return;

            Application.logMessageReceived += HandleLog;

            mOutPath = Application.persistentDataPath + "/outLog.txt";

            //备份之前保存的Log
            if (System.IO.File.Exists(mOutPath)){
                System.IO.File.Copy(mOutPath, Application.persistentDataPath + "/outLog" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            }

            Debug.Log("开启OutLog mOutPath:" + mOutPath);
        }
        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (!logExport) return;
           
            if (type == LogType.Error || type == LogType.Exception)
            {
                mWriteTxt.Append(logString + "\n" + stackTrace + "\r\n");
                WriteIO();
            }
            else
            {
                mWriteTxt.Append(logString + "\r\n");
            }
        }
        private void AddLog(string logString, object stackTrace, LogType type)
        {
            if (!logExport) return;

            mWriteTxt.Append(logString + "\r\n");
        }

        private void WriteIO()
        {
            if (isOverWrite)
            {
                var st = File.OpenWrite(mOutPath);
                var s = System.Text.Encoding.Default.GetBytes(mWriteTxt.ToString());
                st.Write(s, 0, s.Length);
                st.Close();
            }
            else
            {
                //备份之前保存的Log
                if (System.IO.File.Exists(mOutPath)){
                    System.IO.File.Copy(mOutPath, Application.persistentDataPath + "/outLog" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
                }
                File.Create(mOutPath);
                File.WriteAllText(mOutPath, mWriteTxt.ToString());
                mWriteTxt = new StringBuilder();
                UnityEngine.Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }
        }

        #region Logs
        public void Log(string message)
        {
            Log(message, null);
        }
        public void Log(object message)
        {
            Log(message.ToString());
        }
        public void Log(object message, UnityEngine.Object context)
        {
            AddLog(message.ToString(), context, LogType.Log);
            Debug.Log("OutLog: " + message ,context);
        }
        public void Log(string message, UnityEngine.Object context)
        {
            AddLog(message, context, LogType.Log);
            Debug.Log("OutLog: " + message ,context);
        }
        public void LogError(string message)
        {
            LogError(message, null);
        }
        public void LogError(object message)
        {
            LogError(message,null);
        }
        public void LogError(string message, UnityEngine.Object context)
        {
            AddLog(message, context, LogType.Error);
            Debug.LogError("OutLog: " + message, context);
        }
        public void LogError(object message, UnityEngine.Object context)
        {
            AddLog(message.ToString(), context, LogType.Error);
            Debug.LogError("OutLog: " + message,context);
        }
        public void LogWarning(string message)
        {
            LogWarning(message, null);
        }

        public void LogWarning(object message)
        {
            LogWarning(message.ToString(), null);
        }

        public void LogWarning(string message, UnityEngine.Object context)
        {
            AddLog(message, context, LogType.Warning);
            Debug.LogWarning("OutLog: " + message, context);
        }
        public void LogWarning(object message, UnityEngine.Object context)
        {
            AddLog(message.ToString(), context, LogType.Warning);
            Debug.LogWarning("OutLog: " + message,context);
        }
        public void LogException(string message)
        {
            LogException(message, null);
        }
        public void LogException(object message)
        {
            LogException(message.ToString(), null);
        }
        public void LogException(string message, UnityEngine.Object context)
        {
            AddLog(message, context, LogType.Exception);
            Debug.Log("OutLog: " + message, context);
        }
        #endregion
    }
}