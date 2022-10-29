using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public GameObject DifficultyPanel;
    // Start is called before the first frame update
    void Start()
    {
        DifficultyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("RealGame");
        DifficultyPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void NormalButton()
    {
        PlayerPrefs.SetFloat("SpawnTime", 1.5f);
        SceneManager.LoadScene("RealGame");
    }

    public void CrazyButton()
    {
        PlayerPrefs.SetFloat("SpawnTime", 0.05f);
        SceneManager.LoadScene("RealGame");
    }
}
