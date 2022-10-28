using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timetext;
    public float timecount = 30f;
    private static TimeManager m_instance;
    public static TimeManager instance
    {       
        get
        {    
            return m_instance;
        }
    }
    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timecount -=  Time.deltaTime;
        timetext.text = "Time:" + timecount.ToString("0.00") + "s";
        //if (timecount < 0)
        //{
        //    GameManager.instance.Restart();
        //}
    }
}
