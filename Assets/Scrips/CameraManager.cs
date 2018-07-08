using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Transform lookAtTarget;
    public Transform frameTarget;
    public float harf = 1000.0f;
    public float damping = 2.0f;
    public bool GoLookAtTarget = false;

    private float r;
    private float si;
    private float co;

    private void Start()
    {


       

    }
    void Update()
    {
        if (GoLookAtTarget)
        {
            float tagx = lookAtTarget.position.x;
            float tagy = lookAtTarget.position.y;
            float tagz = lookAtTarget.position.z;
            r = Mathf.Sqrt(tagx * tagx + tagy * tagy + tagz * tagz);
            si = Mathf.Acos(tagz / r);
            co = Mathf.Atan(tagy / tagx);
            if (!lookAtTarget || !frameTarget)
                return;
            Vector3 position = new Vector3(harf * Mathf.Sin(si) * Mathf.Cos(co), harf * Mathf.Sin(si) * Mathf.Sin(co), harf * Mathf.Cos(si));
            transform.position = position;
            Quaternion rotate = Quaternion.LookRotation(lookAtTarget.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damping);
        }
    }

    void DoLookAt()
    {
        GoLookAtTarget = true;
    }

    void CancelLookAt()
    {
        GoLookAtTarget = true;
    }

    public void SetTarget(GameObject obj)
    {
        lookAtTarget = obj.transform;
        frameTarget = obj.transform;
    }
}
