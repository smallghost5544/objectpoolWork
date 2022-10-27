using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float gunSpeed;
    [SerializeField]
    //private Transform FollowCamera;
    public AnimationControl animatorC;
    Vector3 movedirection;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        animatorC = GetComponent<AnimationControl>();
        rigid = GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 goforward = transform.forward * v;
        Vector3 goright = transform.right * h;
        movedirection = goforward + goright;
        rigid.AddForce(movedirection.normalized * currentSpeed, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        //rigid.AddForce(-Vector3.up * 100f);
        //rigid.velocity = movedirection * currentSpeed * Time.deltaTime * 10f;
        HandingGunSpeedSwitch();
    }

    private void HandingGunSpeedSwitch()
    {
        if (animatorC.gun.activeSelf == false)
        {
            currentSpeed = walkSpeed;
        }
        else
        {
            currentSpeed = gunSpeed;
        }
    }
}
