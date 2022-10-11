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
        //�s���ؼЫe��V�q
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
        //h��mouseX,�������४�k
        Hdegree = h * sensitive;
        //v��mouseY �Φ��W�U��V,�í���L���L�C
        Vdegree += v;
        if (Vdegree >= 60) Vdegree = 60;
        else if (Vdegree <= -50) Vdegree = -50;
    }

    public void GetHVrotate()
    {
        Vector3 Latevec = currentvec;       //�s���ؼЫe��V�q
        Latevec.y = 0.0f;                   //�h�����C
        Vector3 RotatedH = Quaternion.AngleAxis(Hdegree, Vector3.up) * Latevec; //���k��V�q�� �̷�0.0�����פW���� * �V�q
        RotatedH.Normalize();               //�зǤ�
        Vector3 cross = Vector3.Cross(Vector3.up, RotatedH);                    //�~�n�D�s�V�q
        FinalRotate = Quaternion.AngleAxis(-Vdegree, cross) * RotatedH;         //�̲צb�p��W�U����� * ���k����V�q

    }

    public void CameraTransform()
    {
        Vector3 LookPosition = target.position + Vector3.up * Height + Vector3.right * leftrightpos; //�W�[�[���
        LookPoint.position = Vector3.SmoothDamp(LookPoint.position, LookPosition, ref refvelocity, smoothtime);   //smoothdamp�ܥؼЦ�m
        FollowPosition = LookPoint.position - FinalRotate * distance;   //���H��m�� �ؼЦ�m��h ����V�q*�Z��
        transform.position = Vector3.Lerp(transform.position, FollowPosition, 2f);  //�B��Lerp����
    }

    public void SetForwardandReset()
    {
        Vector3 LookVec = LookPoint.position - transform.position;  //���o�ؼ��I����v�����V�q
        transform.forward = LookVec;                                //��v���e��]���ӦV�q
        Hdegree = 0f;                                               //���s���k����
        currentvec = transform.forward;                             //���s�ثe�V�q
    }

}
