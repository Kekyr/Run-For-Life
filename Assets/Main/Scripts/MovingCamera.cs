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
        Target = FindObjectOfType<PlayerController>().transform;
        Offset = transform.position - Target.position;
    }

    
    private void FixedUpdate()
    {
        Vector3 targetPosition = Target.transform.TransformPoint(Offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _cameraVelocity, _smoothTime);  
    }
    
    public void MoveCameraToTheRight(float LineDistance)
    {
        Offset+=Vector3.right*LineDistance;
    }
    
    public void MoveCameraToTheLeft(float LineDistance)
    {
        Offset+=Vector3.left*LineDistance;
    }
}
