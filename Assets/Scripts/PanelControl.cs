using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject EndPanel;
    public Text EndScore;
    public Text TopScore;
    GameManager gm ;
    // Start is called before the first frame update
    void Start()
    {
        EndPanel.SetActive(false);
        gm = GameManager.instance;
        gm.TopScore = PlayerPrefs.GetInt("TopScore");
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.instance.timecount <= 0)
        {
            CheckScore();
            OpenEndPanelPrepare();
        }
    }

    void CheckScore()
    {
        gm.TopScore = Mathf.Max(gm.TopScore, gm.Score);
    }

    void OpenEndPanelPrepare()
    {
        TimeManager.instance.timetext.text = "Time:0.00s";
        Time.timeScale = 0;
        EndPanel.SetActive(true);
        EndScore.text = "TotalScore: " + gm.Score;
        TopScore.text = "TopScore: " + gm.TopScore;
        PlayerPrefs.SetInt("TopScore" , gm.TopScore);
    }
}
