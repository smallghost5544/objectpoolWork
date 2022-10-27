using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timetext;
    public float timecount;
    // Start is called before the first frame update
    void Start()
    {
        timecount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timecount -=  Time.deltaTime;
        timetext.text = "Time:" + timecount.ToString("0.00") + "s";
        if (timecount < 0)
        {
            GameManager.instance.Restart();
        }
    }
}
