using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolQueue<T> where T : MonoBehaviour
{

    public int ObjectOnStage;
    public Queue<T> objectQueue;
    public GameObject thisprefab;
    private static ObjectPoolQueue<T> _instance;
    public static ObjectPoolQueue<T> instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPoolQueue<T>();
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    public int QueueCount
    {
        get
        {
            return objectQueue.Count;
        }
    }

    public void InitPool(GameObject Prefab)
    {
        thisprefab = Prefab;
        objectQueue = new Queue<T>();
    }

    public T Spawn(Vector3 position, Quaternion rotation)
    {
        if (thisprefab == null)
        {
            Debug.LogError("Instantiate not working");
            return default(T);
        }

        if (QueueCount <= 0)
        {
            GameObject g = Object.Instantiate(thisprefab, position, rotation);
            T t = g.GetComponent<T>();
            if (t == null)
            {
                Debug.LogError("Prefab not find");
                return default(T);
            }
            objectQueue.Enqueue(t);
        }
        T obj = objectQueue.Dequeue();
        obj.gameObject.transform.position = position;
        obj.gameObject.transform.rotation = rotation;
        obj.gameObject.SetActive(true);
        ObjectOnStage++;
        return obj;
    }

    public void Recycle(T obj)
    {
        objectQueue.Enqueue(obj);
        obj.gameObject.SetActive(false);
        ObjectOnStage--;
    }

    public void WarmUp(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject g = Object.Instantiate(thisprefab, Vector3.zero, Quaternion.identity);
            T t = g.GetComponent<T>();
            ObjectOnStage++;
            instance.Recycle(t);
        }
    }
}
