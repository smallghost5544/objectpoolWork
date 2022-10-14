using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public float range;
    public Camera ccamera;
    public GameObject ShootFX;
    public Transform GunPoint;
    RaycastHit hit;
    RaycastHit[] hits;
    public Animator animator;
    public float OnReadyAimThick = 2f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        //Debug.DrawRay(ccamera.transform.position, ccamera.transform.forward , Color.black , 2f);
    }

    private void Shoot()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            if (animator.GetBool("IsShoot"))
            {
                //hits = Physics.RaycastAll(ccamera.transform.position, ccamera.transform.forward, 100f);
                //foreach (RaycastHit hit in hits)
                //{
                //    Debug.Log(hit.transform.gameObject.name);
                //    ObjectRecycleFX(hit);
                //}
                if (Physics.SphereCast(ccamera.transform.position, OnReadyAimThick, ccamera.transform.forward, out hit))
                {
                    ObjectRecycleFX(hit);
                }
                return;
            }
            if (Physics.Raycast(ccamera.transform.position, ccamera.transform.forward, out hit, range))
            {
                ObjectRecycleFX(hit);     
            }           
        }
    }

    public void ObjectRecycleFX(RaycastHit hit)
    {
        //RandomWalkObject r = hit.transform.gameObject.GetComponent<RandomWalkObject>();
        if (hit.transform.gameObject.TryGetComponent<RandomWalkObject>(out var r))
        {
            ObjectPoolQueue<RandomWalkObject>.instance.Recycle(r);
            Instantiate(ShootFX, GunPoint);
        }
    }

    
}
