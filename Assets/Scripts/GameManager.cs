using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPoolQueue<RandomWalkObject> chickenpool;
    public GameObject chicken;
    public int WarmupCount = 100;
    public Transform[] InitPoint;
    public float SpawnTimer = 2f;
    #region Singleton
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        chickenpool = ObjectPoolQueue<RandomWalkObject>.instance;
        chickenpool.InitPool(chicken);
        chickenpool.WarmUp(WarmupCount);
        InvokeRepeating("SpawnAutomatically", SpawnTimer, SpawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnOrRecycle();
    }

    public void SpawnAutomatically()
    {
            Transform t = InitPoint[RandomPoint()];
            chickenpool.Spawn(t.transform.position, t.transform.rotation);
    }

    public void SpawnOrRecycle()
    {
        Transform t = InitPoint[RandomPoint()];
        if (Input.GetKey(KeyCode.E))
        {
            chickenpool.Spawn(t.transform.position , t.transform.rotation);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (chickenpool.ObjectOnStage <= 0)
            {
                Debug.Log("There is no chicken");
                return;
            }
            RandomWalkObject r = GameObject.FindWithTag("Chicken").GetComponent<RandomWalkObject>();
            chickenpool.Recycle(r);
        }
    }



    public int RandomPoint()
    {
        int r = Random.Range(0 , InitPoint.Length);
        return r;
    }
}
