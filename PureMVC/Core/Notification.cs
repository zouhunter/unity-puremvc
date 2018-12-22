
using System.Collections;
using System;



namespace PureMVC
{
    public class Notification<T> : INotification<T>
    {
        public T Body { get; set; }
        public int ObserverKey { get; set; }
        public new string ToString
        {
            get
            {
                string msg = "";
                msg += "\nObserverName:" + ObserverKey.ToString();
                msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
                return msg;
            }
        }
        public bool IsUsing { get; set; }

        public bool Destroy { get; set; }
        public virtual void Clear()
        {
            Body = default(T);
            ObserverKey = -1;
            IsUsing = false;
        }

        // ******************************** OBJECT POOL ********************************

        /// <summary>
        /// Allows us to reuse objects without having to reallocate them over and over
        /// </summary>
        private static ObjectPool<Notification<T>> sPool = new ObjectPool<Notification<T>>(1, 1);

        //public static int Length { get { return sPool.Length; } }
        /// <summary>
        /// Pulls an object from the pool.
        /// </summary>
        /// <returns></returns>
        public static Notification<T> Allocate(int ObserverName, T body)
        {
            Notification<T> lInstance = sPool.Allocate();
            if (lInstance == null) { lInstance = new Notification<T>(); }

            lInstance.ObserverKey = ObserverName;
            lInstance.Body = body;

            lInstance.IsUsing = true;
            return lInstance;
        }

        /// <summary>
        /// Returns an element back to the pool.
        /// </summary>
        /// <param name="rEdge"></param>
        public void Release()
        {
            if (this == null) { return; }

            Clear();

            // Make it available to others.
            if (this is Notification<T>)
            {
                sPool.Release((Notification<T>)this);
            }
        }
    }
}