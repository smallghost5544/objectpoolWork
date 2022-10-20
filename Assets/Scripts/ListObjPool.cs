using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListObjPool : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<ObjectPoolQueue<MonoBehaviour>> objcontrol;
    private static ListObjPool _instance;
    public static ListObjPool instance
    {
        get 
        {
            if (_instance == null)
                _instance = new ListObjPool();
                return _instance;
        }
    

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
