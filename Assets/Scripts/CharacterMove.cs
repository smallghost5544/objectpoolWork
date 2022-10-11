using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Transform FollowCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 newfor = FollowCamera.forward;
        newfor.y = 0;
        if (v != 0 || h != 0)
        {
            transform.forward = newfor;
        }

        Vector3 goforward = transform.forward * v * moveSpeed * Time.deltaTime;
        Vector3 goright = transform.right * h * moveSpeed * Time.deltaTime;
        transform.position += goforward + goright;
    }
}
