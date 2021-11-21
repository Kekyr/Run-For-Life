using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private float lerpTime = 1f;
    private float currentLerpTime;
 
    private float moveDistance = 10f;
 
    private  Vector3 startPos;
    private  Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + transform.up * moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            currentLerpTime = 0f;
        }
 
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }
 
        //lerp!
        float perc = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(startPos, endPos, perc);
    }
}
