using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Transform lookAtTarget;
    public Transform frameTarget;
    public float harf = 1000.0f;
    public float damping = 2.0f;
    public bool GoLookAtTarget = false;
    void Update()
    {
        if (GoLookAtTarget)
        {
            if (!lookAtTarget || !frameTarget)
                return;
            Vector3 position = new Vector3(harf * Mathf.Sin(0.7854f) * Mathf.Cos(0.7854f), harf * Mathf.Sin(0.7854f) * Mathf.Sin(0.7854f), harf * Mathf.Cos(0.7854f));
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
