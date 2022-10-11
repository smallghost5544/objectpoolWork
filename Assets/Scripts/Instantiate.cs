using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject tryprefab;
    public int Count;
    // Start is called before the first frame update
    void Start()
    {
        InstanOnce();
    }

    private void InstanOnce()
    {
        for (int i = 0; i <= Count; i++)
        {
            Instantiate(tryprefab, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
