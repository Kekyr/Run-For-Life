using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public Transform Target;
    
    public Vector3 Offset;
    private Vector3 _cameraVelocity;
    private float _smoothTime = 0.005f;
    
    private void Start()
    {
        Target=FindObjectOfType<RigidbodyController>().transform;
        Offset = transform.position - Target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = Target.transform.TransformPoint(Offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _cameraVelocity, _smoothTime);  
    }
    
}
