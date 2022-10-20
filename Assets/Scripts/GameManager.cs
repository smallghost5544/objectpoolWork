using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPoolQueue<RandomWalkObject>[] animalpool = new ObjectPoolQueue<RandomWalkObject>[5];
    int poolCount = 5;
    public GameObject[] Animals;
    int AnimalNum = 0;
    public int WarmupCount = 100;
    public Transform[] InitPoint;
    public float SpawnTimer = 2f;
    #region Singleton
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameManager();
            }
            return m_instance;
        }
    }
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < poolCount; i++)
        {
            animalpool[i] = new ObjectPoolQueue<RandomWalkObject>();
            Debug.Log(i);
        }
        //animalpool = ObjectPoolQueue<RandomWalkObject>.instance;
    }
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            animalpool[i].InitPool(Animals[i]);
            animalpool[i].WarmUp(WarmupCount);
            //InvokeRepeating("SpawnAutomatically", SpawnTimer, SpawnTimer);
        }
            
            StartCoroutine(makeplentyofanimals());
    }

    // Update is called once per frame
    void Update()
    {   

        SpawnOrRecycle();
        

    }
    IEnumerator makeplentyofanimals()
    {   while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnAutomatically(i);
            }
                yield return new WaitForSeconds(SpawnTimer);
        }
    }

    public void SpawnAutomatically(int i )
    {
            Transform t = InitPoint[RandomPoint()];
            animalpool[i].Spawn(t.transform.position, t.transform.rotation);
    }

    public void SpawnOrRecycle()
    {
        Transform t = InitPoint[RandomPoint()];
        if (Input.GetKey(KeyCode.E))
        {
            animalpool[AnimalNum].Spawn(t.transform.position , t.transform.rotation );
        }
        if (Input.GetKey(KeyCode.Q))
        {
            //if (animalpool[AnimalNum].ObjectOnStage <= 0)
            //{
            //    Debug.Log("There is no Animal");
            //    return;
            //}
            RandomWalkObject r = GameObject.FindWithTag("Animal").GetComponent<RandomWalkObject>();
            animalpool[0].Recycle(r);
        }
    }
    public int RandomPoint()
    {
        int r = Random.Range(0 , InitPoint.Length);
        return r;
    }

    public void RecycletoCertainPool(int i , RandomWalkObject r)
    {

        animalpool[i].Recycle(r);
    }
}
