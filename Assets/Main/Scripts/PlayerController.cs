using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{

    public MovingCamera MovingCamera;
    private CharacterController _characterController;
    private Animator _animator;

    public float LineDistance = 4;
    [SerializeField] private int _speed;
    [SerializeField] private int _jumpForce;
    [SerializeField] private int _fallForce;
    private int _currentLine = 1;
    private Vector3 _dir;



    private void Start()
    {
        _dir = new Vector3();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        CheckSwipeUp();

        CheckSwipeRight();

        CheckSwipeLeft();

        ChangePlayerPosition();

    }




    private void FixedUpdate()
    {
        _dir.z = _speed;
        _dir.y += _fallForce * Time.fixedDeltaTime;
        _characterController.Move(_dir * Time.fixedDeltaTime);
        _animator.SetBool("IsJumping", false);
    }

    private void CheckSwipeUp()
    {
        if (SwipeController.SwipeUp)
        {
            if (_characterController.isGrounded)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        _dir.y = _jumpForce;
        _animator.SetBool("IsJumping", true);
    }

    private void CheckSwipeRight()
    {
        if (SwipeController.SwipeRight)
        {
            if (_currentLine < 2)
            {
                _currentLine++;
                MovingCamera.MoveCameraToTheRight(LineDistance);
            }
        }
    }

    private void CheckSwipeLeft()
    {
        if (SwipeController.SwipeLeft)
        {
            if (_currentLine > 0)
            {
                _currentLine--;
                MovingCamera.MoveCameraToTheLeft(LineDistance);
            }
        }
    }

    private void ChangePlayerPosition()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        targetPosition = CheckCurrentLine(targetPosition);

        transform.position = targetPosition;
    }

    private Vector3 CheckCurrentLine(Vector3 targetPosition)
    {
        if (_currentLine == 0)
        {
            targetPosition += Vector3.left * LineDistance;
        }
        else if (_currentLine == 2)
        {
            targetPosition += Vector3.right * LineDistance;
        }

        return targetPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SceneManager.LoadScene("Main/Scenes/Lost");
        }
    }
}
