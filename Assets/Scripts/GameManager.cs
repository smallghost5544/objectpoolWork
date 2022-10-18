using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPoolQueue<RandomWalkObject> animalpool;  
    public GameObject Animal;
    public int AnimalNum;
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
    private void Awake()
    {
        animalpool = ObjectPoolQueue<RandomWalkObject>.instance;
    }
    void Start()
    {
        animalpool.InitPool(Animal , AnimalNum);
        animalpool.WarmUp(WarmupCount , AnimalNum);
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
            animalpool.Spawn(t.transform.position, t.transform.rotation , AnimalNum);
    }

    public void SpawnOrRecycle()
    {
        Transform t = InitPoint[RandomPoint()];
        if (Input.GetKey(KeyCode.E))
        {
            animalpool.Spawn(t.transform.position , t.transform.rotation , AnimalNum);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (animalpool.ObjectOnStage <= 0)
            {
                Debug.Log("There is no Animal");
                return;
            }
            RandomWalkObject r = GameObject.FindWithTag("Animal").GetComponent<RandomWalkObject>();
            animalpool.Recycle(r);
        }
    }
    public int RandomPoint()
    {
        int r = Random.Range(0 , InitPoint.Length);
        return r;
    }
}
