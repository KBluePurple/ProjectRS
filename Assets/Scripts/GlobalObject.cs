using System.Collections;
using UnityEngine;

class GlobalObject : MonoBehaviour
{
    private static GlobalObject instance;
    public static GlobalObject Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GlobalObject>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GlobalObject");
                    instance = obj.AddComponent<GlobalObject>();
                }
            }
            return instance;
        }
    }

    public new Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return base.StartCoroutine(coroutine);
    }
}