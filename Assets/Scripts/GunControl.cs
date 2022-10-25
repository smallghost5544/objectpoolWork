
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

    public float rr = 0.5f;
    public float bullets;
    public GameObject hittt;
    public GameManager gm;
    public TrailRenderer bullettrail;
    //public RandomWalkObject rwo;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        //if(hittt.transform.gameObject.TryGetComponent<RandomWalkObject>(out var t))
        //rwo = t;
        //Debug.DrawRay(ccamera.transform.position, ccamera.transform.forward , Color.black , 2f);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (animator.GetBool("IsShoot"))
            {
                if (Physics.Raycast(ccamera.transform.position, ccamera.transform.forward, out hit, range))
                {
                    hittt = hit.transform.gameObject;
                    ObjectRecycleFX(hit);
                    TrailRenderer tr = Instantiate(bullettrail, GunPoint.transform.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(tr , hit));
                }
                for (int i = 0; i < bullets - 1; i++)
                {
                    if (Physics.Raycast(ccamera.transform.position, RandomShootPoint(), out hit, range))
                    {
                        ObjectRecycleFX(hit);
                        TrailRenderer tr = Instantiate(bullettrail, GunPoint.transform.position, Quaternion.identity);
                        StartCoroutine(SpawnTrail(tr, hit));
                    }
                }
                //hits = Physics.RaycastAll(ccamera.transform.position, ccamera.transform.forward, 100f);
                //foreach (RaycastHit hit in hits)
                //{
                //    Debug.Log(hit.transform.gameObject.name);
                //    ObjectRecycleFX(hit);
                //}
                //if (Physics.SphereCast(ccamera.transform.position, OnReadyAimThick, ccamera.transform.forward, out hit))
                //{
                //    ObjectRecycleFX(hit);
                //}
                //return;
            }
            //if (Physics.Raycast(ccamera.transform.position, ccamera.transform.forward, out hit, range))
            //{
            //    ObjectRecycleFX(hit);     
            //}           
        }
    }

    public void ObjectRecycleFX(RaycastHit hit)
    {
        //RandomWalkObject r = hit.transform.gameObject.GetComponent<RandomWalkObject>();
        if (hit.transform.gameObject.TryGetComponent<RandomWalkObject>(out var r))
        {
            //ObjectPoolQueue<RandomWalkObject>.instance.Recycle(r);
            r.RecycleSelf();
            Instantiate(ShootFX, GunPoint);
            //gm.animalpool[r.AnimalNum].Recycle(r);
        }
    }

    IEnumerator SpawnTrail(TrailRenderer trail , RaycastHit hit)
    {
        float time = 0;
        Vector3 StartPosition = trail.transform.position;
        while (time < 0.7f)
        {
            trail.transform.position = Vector3.Lerp(StartPosition ,hit.point,time*3);
            time += Time.deltaTime;
            yield return null;
        }
        trail.transform.position = hit.point;
        
    }

    Vector3 RandomShootPoint()
    {
        Vector3 TargetPos = ccamera.transform.position + ccamera.transform.forward;
        TargetPos = new Vector3(TargetPos.x + Random.Range(-rr , rr),
                                TargetPos.y + Random.Range(-rr, rr) ,
                                TargetPos.z + Random.Range(-rr, rr));
        Vector3 dir = TargetPos - ccamera.transform.position;
        return dir.normalized;
    }
}
