using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float lerpTime = 1f;
    private float currentLerpTime;
    
    
    public float LineDistance = 4;
    
    [SerializeField] private int speed;
    private CharacterController controller;

    [SerializeField] private Transform camera;
    private Vector3 targetCameraPosition;
    private Vector3 _cameraVelocity;
    [SerializeField] private float _smoothTime = 0.004f;

    private Vector3 dir;

    private static int currentLine = 1;
    
    private void Start()
    {
        dir = Vector3.forward;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
       
        
        if (SwipeController.swipeRight)
        {
            if (currentLine < 2)
            {
                currentLine++;
            }
        }

        if (SwipeController.swipeLeft)
        {
            if (currentLine > 0)
            {
                currentLine--;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (currentLine == 0)
        {
            targetPosition += Vector3.left * LineDistance;
            targetCameraPosition = camera.position+(Vector3.left * LineDistance);
            camera.position = Vector3.SmoothDamp(camera.position, targetCameraPosition, ref _cameraVelocity, _smoothTime);

            //float perc = currentLerpTime / lerpTime;
            //camera.position = Vector3.Lerp(camera.position, targetCameraPosition, perc);
        }
        else if (currentLine == 2)
        {
            targetPosition += Vector3.right * LineDistance;
            targetCameraPosition = camera.position+(Vector3.right * LineDistance);
            camera.position = Vector3.SmoothDamp(camera.position, targetCameraPosition, ref _cameraVelocity, _smoothTime);

        }

        transform.position = targetPosition;
    }

   
    private void FixedUpdate()
    {
        dir.z = speed;
        controller.Move(dir * Time.fixedDeltaTime);
    }
}
