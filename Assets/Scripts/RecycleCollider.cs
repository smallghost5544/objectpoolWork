using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Animal")
        {
            var r = other.GetComponent<RandomWalkObject>();
            GameManager.instance.RecycletoCertainPool(r.AnimalNum ,r );
        }
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.Restart();
        }
    }
}
