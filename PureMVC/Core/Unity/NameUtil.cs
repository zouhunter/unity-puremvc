
using System.Collections;
using System.Reflection;
using System;

namespace PureMVC
{
    public partial class NameUtil
    {
        /// <summary>
        /// 快速设置public,static类型的属性字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void InitProperties<T>()
        {
            var fields = typeof(T).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
            foreach (var item in fields)
            {
                item.SetValue(null, item.Name, null);
            }
        }
    }
}