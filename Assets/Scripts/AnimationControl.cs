using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator animator;
    public GameObject gun;
    public GameObject backgun;
    public GameObject ControlRig;

    public float rigx;
    public float rigy;
    public float rigz;
    public Transform originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        backgun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DrawandHideGun();
        Move();
        ReadyShoot();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RotateRig();
            animator.SetTrigger("Shoot");
            Invoke("EndRotate", 0.5f);
        }
    }

    private void ReadyShoot()
    {
        if (Input.GetMouseButtonDown(1) && gun.activeSelf == true)
        {
            if (animator.GetBool("IsShoot"))
            {
                EndRotate();
                animator.SetBool("IsShoot", false);
            }
            else if (!animator.GetBool("IsShoot"))
            {
                RotateRig();
                animator.SetBool("IsShoot", true);
            }
        }
    }

    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            animator.SetBool("IsWalk", true);
        }
        else
            animator.SetBool("IsWalk", false);
    }

    public void DrawandHideGun()
    {
        if (Input.GetKeyDown(KeyCode.C) && !animator.GetBool("IsShoot"))
        {
            if (gun.activeSelf == false) //沒槍時取槍並顯示
            {
                animator.SetTrigger("draw");
                animator.SetInteger("IdleStyle", 0);
                //animator.SetLayerWeight(1, 1);
            }
            else                     //有槍時收槍並隱藏
            {
                animator.SetTrigger("hide");
                animator.SetInteger("IdleStyle", 1);
                //animator.SetLayerWeight(1, 1);
            }

            Invoke("GunActive", 0.75f);
            // Invoke("SetLayerWeightZero", 2.25f);
        }
    }

    void GunActive()
    {
        if (gun.activeSelf == true)
        {
            gun.SetActive(false);
            backgun.SetActive(true);
        }
        else
        {
            gun.SetActive(true);
            backgun.SetActive(false);
        }
    }

    void SetLayerWeightZero()
    {
        animator.SetLayerWeight(1, Mathf.Lerp(0, 0.65f, 1f));

    }

     public void RotateRig()
    {
        originalRotation.rotation = ControlRig.transform.rotation;
        ControlRig.transform.Rotate(rigx,rigy, rigz, Space.Self);
    }
    public void EndRotate()
    {
        ControlRig.transform.Rotate(-rigx, -rigy, -rigz, Space.Self);
        //ControlRig.transform.rotation = Quaternion.Lerp(ControlRig.transform.rotation, originalRotation.rotation, Time.time * 0.7f);

    }
}
