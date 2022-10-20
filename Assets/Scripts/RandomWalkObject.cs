using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkObject : MonoBehaviour
{
    Vector3 currentvec;
    public Animator animator;
    public float MoveSpeed = 1.5f;
    public int AnimalNum;
    GameManager gm;
    public Vector3 targetpoint;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        animator = GetComponent<Animator>();
        PickPointori();
    }

    // Update is called once per frame
    void Update()
    {
        currentvec = targetpoint - transform.position;
        currentvec.y = 0;
        Move();    
    }

    public void PickPointori()
    {
        int r = Random.Range(-20, 20);
        int z = Random.Range(-20, 20);
        targetpoint = new Vector3(r, 0, z);
    }

    public IEnumerator PickPoint()
    {
        int r = Random.Range(-50, 50);
        int z = Random.Range(-50, 50);
        targetpoint = new Vector3(r, transform.position.y, z);
        yield return new WaitForSeconds(0f);
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetpoint, MoveSpeed*Time.deltaTime);
        if (currentvec.magnitude > 0)
        {
            animator.SetInteger("Walk", 1);
            transform.forward = currentvec;
        }
        else
        {
            animator.SetInteger("Walk", 0);
            StartCoroutine(PickPoint());
        }
    }

    public void RecycleSelf()
    {
        //GameManager.instance.RecycletoCertainPool(AnimalNum , this);
        gm.RecycletoCertainPool(AnimalNum , this);
    }
}
