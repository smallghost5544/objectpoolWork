using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstIntro : MonoBehaviour
{
    public GameObject guidepanel;
    // Start is called before the first frame update
    void Start()
    {
        guidepanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenGuide()
    {
        guidepanel.SetActive(true);
    }

    public void Close()
    {
        guidepanel.SetActive(false);
    }
}
