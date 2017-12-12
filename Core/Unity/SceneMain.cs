using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

    public class SceneMain : MonoBehaviour
    {
        public interface IEventItem
        {
            object Action { get; }
            void Release();
        }

        public class EventItem : IEventItem
        {
            public UnityAction action;
            public object Action {
                get { return action; }
            }
            private static ObjectPool<EventItem> sPool = new ObjectPool<EventItem>(1, 1);

            public static EventItem Allocate(UnityAction action)
            {
               var item = sPool.Allocate();
                item.action = action;
                return item;
            }
            public void Release()
            {
                sPool.Release(this);
            }
        }

        public class EventItem<T> : IEventItem
        {
            public UnityAction<T> action;
            public object Action
            {
                get { return action; }
            }
            private static ObjectPool<EventItem<T>> sPool = new ObjectPool<EventItem<T>>(1, 1);

            public static EventItem<T> Allocate(UnityAction<T> action)
            {
                var item = sPool.Allocate();
                item.action = action;
                return item;
            }
            public void Release()
            {
                sPool.Release(this);
            }
        }

        public class EventHold
        {
            protected IDictionary<string, List<IEventItem>> m_observerMap;

            public UnityAction<string> MessageNotHandled { get; set; }
            public EventHold()
            {
                m_observerMap = new Dictionary<string, List<IEventItem>>();
            }
            public void NoMessageHandle(string rMessage)
            {
                if (MessageNotHandled == null)
                {
                    Debug.LogWarning("MessageDispatcher: Unhandled Message of type " + rMessage);
                }
                else
                {
                    MessageNotHandled(rMessage);
                }
            }

            #region 注册注销事件
            public void AddDelegate<T>(string key, UnityAction<T> handle)
            {
                if (handle == null) return;
                EventItem<T> observer = EventItem<T>.Allocate(handle);
                RegisterObserver(key, observer);
            }
            public void AddDelegate(string key, UnityAction handle)
            {
                if (handle == null) return;
                EventItem observer = EventItem.Allocate(handle);
                RegisterObserver(key, observer);
            }

            public void RemoveDelegate<T>(string key, UnityAction<T> handle)
            {
                ReMoveObserver(key, handle);
            }
            public void RemoveDelegate(string key, UnityAction handle)
            {
                 ReMoveObserver(key, handle);
            }
            public void RemoveDelegates(string key)
            {
                if (m_observerMap.ContainsKey(key))
                {
                    m_observerMap.Remove(key);
                }
            }
            #endregion

            #region 触发事件
            public void NotifyObserver(string key)
            {
                if (m_observerMap.ContainsKey(key))
                {
                    var list = m_observerMap[key];
                    foreach (var item in list)
                    {
                        if (item is EventItem)
                        {
                            (item as EventItem).action.Invoke();
                        }
                        else
                        {
                            NoMessageHandle(key);
                        }
                    }

                }
                else
                {
                    NoMessageHandle(key);
                }
            }
            public void NotifyObserver<T>(string key, T value)
            {
                if(key == EventKeys.AppendScore)
                {
                    Debug.Log("[AppendScore]" + m_observerMap.ContainsKey(key) + ":" + m_observerMap[key].Count);
                }
                if (m_observerMap.ContainsKey(key))
                {
                    var list = m_observerMap[key];
                    var actions = list.FindAll(x => x is EventItem<T>);
                    foreach (var item in actions){
                        (item as EventItem<T>).action.Invoke(value);
                    }
                }
                else
                {
                    NoMessageHandle(key);
                }
            }
            #endregion

            private void RegisterObserver(string key, IEventItem observer)
            {
                if (m_observerMap.ContainsKey(key))
                {
                    if (!m_observerMap[key].Contains(observer))
                    {
                        m_observerMap[key].Add(observer);
                    }
                }
                else
                {
                    m_observerMap.Add(key, new List<IEventItem>() { observer });
                }
            }

            private bool ReMoveObserver(string key, object handle)
            {
                if (handle == null) return false;
                if (m_observerMap.ContainsKey(key))
                {
                    var list = m_observerMap[key];
                    var item = list.Find(x => object.Equals(x.Action,handle));
                    if (item != null)
                    {
                        item.Release();
                        list.Remove(item);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public class ObjectContainer
        {
            private MonoBehaviour _holder;
            private Transform _transform;
            private float cachingTime = 30f;
            //创建对象池字典
            private Dictionary<string, List<GameObject>> poolObjs = new Dictionary<string, List<GameObject>>();

            private Dictionary<GameObject, float> poolObjTimes = new Dictionary<GameObject, float>();

            private List<GameObject> destroyedObjects = new List<GameObject>();

            private Coroutine updateCo;

            public ObjectContainer(MonoBehaviour holder)
            {
                this._holder = holder;
                this._transform = holder.GetComponent<Transform>();// _transform;
            }

            /// <summary>
            /// 用于创建静止的物体，指定父级、坐标
            /// </summary>
            /// <returns></returns>
            public GameObject GetPoolObject(GameObject pfb, Transform parent, bool world, bool resetLocalPosition = true, bool resetLocalScale = false, bool activepfb = false)
            {
                pfb.SetActive(true);
                GameObject currGo;
                ////Debug.Log(pfb.name);
                //如果有预制体为名字的对象小池
                if (poolObjs.ContainsKey(pfb.name))
                {
                    List<GameObject> currentList = null;
                    currentList = poolObjs[pfb.name];
                    destroyedObjects.Clear();
                    //遍历每数组，得到一个隐藏的对象
                    for (int i = 0; i < currentList.Count; i++)
                    {
                        if (currentList[i] == null)
                        {
                            destroyedObjects.Add(currentList[i]);
                            continue;
                        }
                        if (!currentList[i].activeSelf)
                        {
                            currentList[i].SetActive(true);
                            currentList[i].transform.SetParent(parent, world);
                            if (resetLocalPosition)
                            {
                                currentList[i].transform.localPosition = Vector3.zero;
                            }
                            if (resetLocalScale)
                            {
                                currentList[i].transform.localScale = Vector3.one;
                            }
                            pfb.SetActive(activepfb);
                            poolObjTimes.Remove(currentList[i]);
                            return currentList[i];
                        }
                    }
                    //当没有隐藏对象时，创建一个并返回
                    currGo = CreateAGameObject(pfb, parent, world, resetLocalPosition, resetLocalScale);
                    currentList.Add(currGo);
                    pfb.SetActive(activepfb);
                    return currGo;
                }
                currGo = CreateAGameObject(pfb, parent, world, resetLocalPosition, resetLocalScale);
                //如果没有对象小池
                poolObjs.Add(currGo.name, new List<GameObject>() { currGo });
                pfb.SetActive(activepfb);
                return currGo;
            }

            public GameObject GetPoolObject(string pfbName, Transform parent, bool world, bool resetLocalPosition = true, bool resetLocalScale = false)
            {
                if (poolObjs.ContainsKey(pfbName))
                {
                    List<GameObject> currentList = null;
                    currentList = poolObjs[pfbName];
                    //遍历每数组，得到一个隐藏的对象
                    for (int i = 0; i < currentList.Count; i++)
                    {
                        if (!currentList[i].activeSelf)
                        {
                            currentList[i].SetActive(true);
                            currentList[i].transform.SetParent(parent, world);
                            if (resetLocalPosition)
                            {
                                currentList[i].transform.localPosition = Vector3.zero;
                            }
                            if (resetLocalScale)
                            {
                                currentList[i].transform.localScale = Vector3.one;
                            }
                            poolObjTimes.Remove(currentList[i]);
                            return currentList[i];
                        }
                    }
                }

                return null;
            }

            GameObject CreateAGameObject(GameObject pfb, Transform parent, bool world, bool resetLocalPositon, bool resetLocalScale)
            {
                GameObject currentGo = GameObject.Instantiate(pfb);
                currentGo.name = pfb.name;
                currentGo.transform.SetParent(parent, world);
                if (resetLocalPositon)
                {
                    currentGo.transform.localPosition = Vector3.zero;
                }
                if (resetLocalScale)
                {
                    currentGo.transform.localScale = Vector3.one;
                }
                return currentGo;
            }

            IEnumerator UpdatePool()
            {
                while (true)
                {
                    if (poolObjTimes.Count == 0)
                    {
                        updateCo = null;
                        yield break;
                    }

                    GameObject destroyGo = null;
                    foreach (var pair in poolObjTimes)
                    {
                        if (Time.time - pair.Value > cachingTime)
                        {
                            destroyGo = pair.Key;
                            break;
                        }
                    }

                    if (destroyGo != null)
                    {
                        var currList = poolObjs[destroyGo.name];
                        currList.Remove(destroyGo);
                        if (currList.Count == 0)
                        {
                            poolObjs.Remove(destroyGo.name);
                        }
                        poolObjTimes.Remove(destroyGo);
                    }

                    yield return new WaitForSeconds(0.5f);
                }
            }

            public void SavePoolObject(GameObject go, bool world = false)
            {
                if (poolObjs.ContainsKey(go.name))
                {
                    var currList = poolObjs[go.name];
                    if (!currList.Contains(go))
                    {
                        currList.Add(go);
                    }
                }
                else
                {
                    poolObjs.Add(go.name, new List<GameObject> { go });
                }

                go.transform.SetParent(_transform, world);
                go.SetActive(false);
                poolObjTimes.Add(go, Time.time);
                if (updateCo == null)
                {
                    updateCo = _holder.StartCoroutine(UpdatePool());
                }
            }
        }

        protected static SceneMain _abstruct;

        public static SceneMain Current
        {
            get
            {
                return _abstruct;
            }
        }
        private ObjectContainer _container;
        private EventHold _eventHold;
        protected virtual void Awake()
        {
            _eventHold = new EventHold();
            _container = new ObjectContainer(this);
            _abstruct = this;
        }
        #region 访问事件系统
        public void RegisterEvent(string noti, UnityAction even)
        {
            _eventHold.AddDelegate(noti, even);
        }
        public void RegisterEvent<T>(string noti, UnityAction<T> even)
        {
            _eventHold.AddDelegate(noti, even);
        }

        public void RemoveEvent(string noti, UnityAction even)
        {
            _eventHold.RemoveDelegate(noti, even);
        }
        public void RemoveEvent<T>(string noti, UnityAction<T> even)
        {
            _eventHold.RemoveDelegate(noti, even);
        }

        public void RemoveEvents(string noti)
        {
            _eventHold.RemoveDelegates(noti);
        }

        public void InvokeEvents(string noti)
        {
            _eventHold.NotifyObserver(noti);
        }
        public void InvokeEvents<T>(string noti, T data)
        {
            _eventHold.NotifyObserver<T>(noti, data);
        }
        #endregion

        #region  访问对象池
        public GameObject GetPoolObject(GameObject pfb, Transform parent, bool world, bool resetLocalPosition = true, bool resetLocalScale = false, bool activepfb = false)
        {
            return _container.GetPoolObject(pfb, parent, world, resetLocalPosition, resetLocalScale, activepfb);
        }
        public GameObject GetPoolObject(string pfbName, Transform parent, bool world, bool resetLocalPosition = true, bool resetLocalScale = false)
        {
            return _container.GetPoolObject(pfbName, parent, world, resetLocalPosition, resetLocalScale);
        }
        public void SavePoolObject(GameObject go, bool world = false)
        {
            _container.SavePoolObject(go, false);
        }
        #endregion
    }

    public class SceneMain<T> : SceneMain where T: SceneMain
    {
        public new static T Current { get { return (T)_abstruct; } }

        protected override void Awake()
        {
            base.Awake();
        }
    }

