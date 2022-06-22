using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ObjectPoolManager<T> where T : MonoBehaviour, IPoolable
{
    private static bool IsInitialized = false;
    private static GameObject _prefab = null;
    private static Dictionary<GameObject, bool> _pooledDict = new Dictionary<GameObject, bool>();
    private static Queue<GameObject> _objectQueue = new Queue<GameObject>();
    private static Transform _poolStore = null;

    public static int Count => _objectQueue.Count;

    static ObjectPoolManager()
    {
        Debug.Log($"{typeof(ObjectPoolManager<T>).Name}: Initialize");
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        _prefab = Resources.Load<GameObject>("Prefabs/" + typeof(T).Name);
        IsInitialized = true;
    }

    private static void ActiveSceneChanged(Scene prev, Scene scene)
    {
        _poolStore = GameObject.Find("PoolStore").transform;
        if (_poolStore == null)
        {
            GameObject gameObject = new GameObject("PoolStore");
            _poolStore = gameObject.transform;
        }
        _objectQueue.Clear();
    }

    public static T Get()
    {
        T pool = null;
        if (_objectQueue.Count > 0)
        {
            pool = _objectQueue.Dequeue().GetComponent<T>();
        }
        else
        {
            GameObject gameObject = GameObject.Instantiate(_prefab);
            gameObject.name = typeof(T).Name + "";
            pool = gameObject.GetComponent<T>();
        }
        _pooledDict[pool.gameObject] = false;
        pool.Initialize();
        pool.gameObject.SetActive(true);
        return pool;
    }

    public static void Put(T pool)
    {
        bool isPooled = false;
        if (_pooledDict.TryGetValue(pool.gameObject, out isPooled))
        {
            if (isPooled)
            {
                Debug.LogError($"{pool.gameObject.name} is not valid object");
                return;
            }
            pool.gameObject.SetActive(false);
            _objectQueue.Enqueue(pool.gameObject);
        }
        else
        {
            Debug.LogError($"{pool.gameObject.name} is not object in pool");
        }
    }
}