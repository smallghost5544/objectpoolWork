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

    // Start is called before the first frame update
    void Start()
    {
        animatorC = GetComponent<AnimationControl>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        

        Vector3 goforward = transform.forward * v * currentSpeed * Time.deltaTime;
        Vector3 goright = transform.right * h * currentSpeed * Time.deltaTime;
        transform.position += goforward + goright;

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
