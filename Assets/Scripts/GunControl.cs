
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
    public Animator animator;

    public float rr = 0.5f;
    public float bullets;
    public TrailRenderer bullettrail;
    public Queue<TrailRenderer> trqueue;

    void Start()
    {
        animator = GetComponent<Animator>();
        trqueue = new Queue<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
      
    }

    private void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (animator.GetBool("IsShoot"))
            {
                if (Physics.Raycast(ccamera.transform.position, ccamera.transform.forward, out hit, range))
                {
                    ObjectRecycleFX(hit);           
                    TrailRenderer tr = ObjPoolSpawn(GunPoint.transform.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(tr , hit));
                }
                for (int i = 0; i < bullets - 1; i++)
                {
                    if (Physics.Raycast(ccamera.transform.position, RandomShootPoint(), out hit, range))
                    {
                        ObjectRecycleFX(hit);                      
                        TrailRenderer tr = ObjPoolSpawn(GunPoint.transform.position, Quaternion.identity);
                        StartCoroutine(SpawnTrail(tr, hit));
                    }
                }            
            }         
        }
    }

    public void ObjectRecycleFX(RaycastHit hit)
    {
        if (hit.transform.gameObject.TryGetComponent<RandomWalkObject>(out var r))
        {
            r.RecycleSelf();
            Instantiate(ShootFX, GunPoint);
        }
    }

    IEnumerator SpawnTrail(TrailRenderer trail , RaycastHit hit)
    {
        float time = 0;
        Vector3 StartPosition = trail.transform.position;
        while (time < 0.7f)
        {
            trail.transform.position = Vector3.Lerp(StartPosition ,hit.point,time*4);
            time += Time.deltaTime;
            yield return null;
        }
        trail.transform.position = hit.point;
        Recycle(trail);
    }

    public TrailRenderer ObjPoolSpawn(Vector3 position, Quaternion rotation)
    {
        if (trqueue.Count <= 0)
        {
            TrailRenderer g = Instantiate(bullettrail, position, rotation);
            TrailRenderer t = g.GetComponent<TrailRenderer>();
            if (t == null)
            {
                Debug.LogError("Prefab not find");
                return default(TrailRenderer);
            }
            trqueue.Enqueue(t);
        }
        TrailRenderer obj = trqueue.Dequeue();
        obj.gameObject.transform.position = position;
        obj.gameObject.transform.rotation = rotation;
        obj.gameObject.SetActive(true);

        return obj;
    }

    public void Recycle(TrailRenderer t)
    {
        t.gameObject.SetActive(false);
        trqueue.Enqueue(t);
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
