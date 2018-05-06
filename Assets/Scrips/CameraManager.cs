using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Transform lookAtTarget;
    public Transform frameTarget;
    public float damping = 2.0f;
    public bool GoLookAtTarget = false;
    void Update()
    {
        if (GoLookAtTarget)
        {
            if (!lookAtTarget || !frameTarget)
                return;
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
}
