using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Animal")
        {
            var r = other.GetComponent<RandomWalkObject>();
            GameManager.instance.RecycletoCertainPool(r.AnimalNum ,r );
        }
        if (other.gameObject.tag == "Player")
        {
            //restart
        }
    }
}
