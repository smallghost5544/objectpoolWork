using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCameraMove : MonoBehaviour
{
    public Vector3 currentvec;
    public Transform target;
    public Transform LookPoint;
    public float sensitive;
    public float distance = 5f;
    public float Height = 2;
    public float leftrightpos = 3;
    public float smoothtime = 0.6f;
    Vector3 refvelocity = Vector3.zero;
    Vector3 FollowPosition = Vector3.zero;
    Vector3 FinalRotate = Vector3.zero;
    float Hdegree = 0f;
    float Vdegree = 0f;
    // Start is called before the first frame update
    void Start()
    {   
        //存取目標前方向量
        //currentvec = target.forward;
    }

    // Update is called once per frame
    void Update()
    {
        SetDegree();
    }

    private void LateUpdate()
    {
        GetHVrotate();
        CameraTransform();
        SetForwardandReset();
    }



    public void SetDegree()
    {   
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        //h為mouseX,直接旋轉左右
        Hdegree = h * sensitive;
        //v為mouseY 形成上下轉向,並限制過高過低
        Vdegree += v;
        if (Vdegree >= 60) Vdegree = 60;
        else if (Vdegree <= -50) Vdegree = -50;
    }

    public void GetHVrotate()
    {
        Vector3 Latevec = currentvec;       //存取目標前方向量
        Latevec.y = 0.0f;                   //去除高低
        Vector3 RotatedH = Quaternion.AngleAxis(Hdegree, Vector3.up) * Latevec; //左右轉向量為 依照0.0做角度上旋轉 * 向量
        RotatedH.Normalize();               //標準化
        Vector3 cross = Vector3.Cross(Vector3.up, RotatedH);                    //外積求新向量
        FinalRotate = Quaternion.AngleAxis(-Vdegree, cross) * RotatedH;         //最終在計算上下旋轉輛 * 左右旋轉向量

    }

    public void CameraTransform()
    {
        Vector3 LookPosition = target.position + Vector3.up * Height + Vector3.right * leftrightpos; //增加觀察高度
        LookPoint.position = Vector3.SmoothDamp(LookPoint.position, LookPosition, ref refvelocity, smoothtime);   //smoothdamp至目標位置
        FollowPosition = LookPoint.position - FinalRotate * distance;   //跟隨位置為 目標位置減去 選轉向量*距離
        transform.position = Vector3.Lerp(transform.position, FollowPosition, 2f);  //運用Lerp移動
    }

    public void SetForwardandReset()
    {
        Vector3 LookVec = LookPoint.position - transform.position;  //取得目標點至攝影機之向量
        transform.forward = LookVec;                                //攝影機前方設為該向量
        Hdegree = 0f;                                               //重製左右角度
        currentvec = transform.forward;                             //重製目前向量
    }

}
