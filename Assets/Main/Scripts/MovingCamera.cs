using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public Transform _target;
    private Vector3 _offset = new Vector3(-0.57f, 3.354f, -3.32f);
    private Vector3 _cameraVelocity;
    private float _smoothTime = 0.001f;
    


    private void Start()
    {
        _target = FindObjectOfType<PlayerController>().transform;
    }

    
    private void FixedUpdate()
    {
        var targetPosition = _target.transform.TransformPoint(_offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _cameraVelocity, _smoothTime);  
    }
}
