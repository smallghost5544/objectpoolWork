using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ObjectPoolQueue<RandomWalkObject>[] animalpool = new ObjectPoolQueue<RandomWalkObject>[5];
    int poolCount = 5;
    public GameObject[] Animals;
    public int WarmupCount = 100;
    public Transform[] InitPoint;
    public float SpawnTimer = 2f;

    public int AnimalOnStage = 0;
    public int LimitCount = 300;
    public int Score = 0;
    public int TopScore = 0;
    public Text scoretext;
    public GameObject RecycleFX;
    public PanelControl panelcontrol;
    #region Singleton
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            return m_instance;
        }
    }
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        } 
        for (int i = 0; i < poolCount; i++)
        {
            animalpool[i] = new ObjectPoolQueue<RandomWalkObject>();
            Debug.Log(i);
        }
        //animalpool = ObjectPoolQueue<RandomWalkObject>.instance;
    }
    void Start()
    {
        SpawnTimer = PlayerPrefs.GetFloat("SpawnTime");
        
        for (int i = 0; i < 3; i++)
        {
            animalpool[i].InitPool(Animals[i]);
            animalpool[i].WarmUp(WarmupCount);
            //InvokeRepeating("SpawnAutomatically", SpawnTimer, SpawnTimer);
        }
            
            StartCoroutine(makeplentyofanimals());
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {   
        SpawnOrRecycle();
        if (Input.GetKeyDown(KeyCode.T))
        {
            Restart();
        }
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
        if (AnimalOnStage < LimitCount)
        {
            animalpool[i].Spawn(InitPoint[i].transform.position, InitPoint[i].transform.rotation);
            AnimalOnStage++;
        }
    }

    public void SpawnOrRecycle()
    {
        Transform t = InitPoint[RandomPoint()];
        
        if (Input.GetKey(KeyCode.E))
        {
            int rx = Random.Range(0, 3);
            animalpool[rx].Spawn(InitPoint[rx].transform.position, InitPoint[rx].transform.rotation);
            AnimalOnStage++;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (AnimalOnStage <= 0)
            {
                Debug.Log("There is no Animal");
                return;
            }
            RandomWalkObject r = GameObject.FindWithTag("Animal").GetComponent<RandomWalkObject>();
            r.RecycleSelf();
            AnimalOnStage--;
        }
    }
    public int RandomPoint()
    {
        int r = Random.Range(0 , InitPoint.Length);
        return r;
    }

    public void RecycletoCertainPool(int i , RandomWalkObject r )
    {
        animalpool[i].Recycle(r);
        Instantiate(RecycleFX, r.gameObject.transform.position, r.gameObject.transform.rotation);
    }

    public void Restart()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

}
