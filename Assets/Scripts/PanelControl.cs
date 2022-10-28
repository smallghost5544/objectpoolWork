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

    public Button Cbutton;
    public Button Nbutton;
    // Start is called before the first frame update
    void Start()
    {
        EndPanel.SetActive(false);
        gm = GameManager.instance;
        gm.TopScore = PlayerPrefs.GetInt("TopScore");
        Cbutton.gameObject.SetActive(true);
        Nbutton.gameObject.SetActive(false);
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
        Time.timeScale = 0.01f;
        EndPanel.SetActive(true);
        EndScore.text = "TotalScore: " + gm.Score;
        TopScore.text = "TopScore: " + gm.TopScore;
        PlayerPrefs.SetInt("TopScore" , gm.TopScore);
    }


    public void ResetScore()
    {
        gm.Score = 0;
        gm.TopScore = 0;
        PlayerPrefs.DeleteKey("TopScore");
        EndScore.text = "TotalScore: " + gm.Score;
        TopScore.text = "TopScore: " + gm.TopScore;
        Debug.Log("hit it");
    }

    public void CrazyMode()
    {
        PlayerPrefs.SetFloat("SpawnTime", 0.2f);
        Cbutton.gameObject.SetActive(false);
        Nbutton.gameObject.SetActive(true);
    }

    public void NormalMode()
    {
        PlayerPrefs.SetFloat("SpawnTime", 1.5f);
        Cbutton.gameObject.SetActive(true);
        Nbutton.gameObject.SetActive(false);
    }
   
}
