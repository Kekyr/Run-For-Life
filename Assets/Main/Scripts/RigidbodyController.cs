using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RigidbodyController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Coroutine _movingCoroutine;
    public MovingCamera MovingCamera;

    private Vector3 _startGamePosition;
    private Vector3 _targetVelocity;
    private Quaternion _startGameRotation;
    private float _distanceBetweenLines=1.5f;
    private float _changeLineSpeed = 30;
    private float _startPoint;
    private float _finishPoint;
    private float _lastVectorX;
    private float _jumpPower = 15;
    private float _jumpGravity = -40;
    private float _normalGravity = -9.8f;
    private bool _isMoving = false;
    private bool _isJumping = false;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startGamePosition = transform.position;
        _startGameRotation = transform.rotation;
    }


    private void Update()
    {
        if (SwipeController.SwipeLeft && _finishPoint>-_distanceBetweenLines)
        {
           MoveHorizontal(-_changeLineSpeed);
        }
        if (SwipeController.SwipeRight && _finishPoint< _distanceBetweenLines)
        {
            MoveHorizontal(_changeLineSpeed);
        }
        if(SwipeController.SwipeUp && _isJumping==false)
        {
            Jump();
        }
    }
    
    private void MoveHorizontal(float speed)
    {
        _startPoint = _finishPoint;
        _finishPoint +=Mathf.Sign(speed) * _distanceBetweenLines;

        if (_isMoving)
        {
            StopCoroutine(_movingCoroutine);
            _isMoving = false;
        }
        _movingCoroutine=StartCoroutine(MoveCoroutine(speed));
    }
    
    private IEnumerator MoveCoroutine(float vectorX)
    {
        _isMoving = true;
        while (Mathf.Abs(_startPoint - transform.position.x) < _distanceBetweenLines)
        {
            yield return new WaitForFixedUpdate();

            Moving(vectorX);
        }

        StopMoving();
    }
    
    private void Moving(float vectorX)
    {
        _rigidbody.velocity = new Vector3(vectorX, _rigidbody.velocity.y, 0);
        _lastVectorX = vectorX;
        float x = Mathf.Clamp(transform.position.x, Mathf.Min(_startPoint, _finishPoint),
            Mathf.Max(_startPoint, _finishPoint));
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    
    private void StopMoving()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = new Vector3(_finishPoint, transform.position.y, transform.position.z);
        _isMoving = false;
    }

    private void Jump()
    {
        _isJumping = true;
        _rigidbody.AddForce(Vector3.up*_jumpPower,ForceMode.Impulse);
        Physics.gravity = new Vector3(0, _jumpGravity, 0);
        StartCoroutine(StopJumpCoroutine());

    }
    
    private IEnumerator StopJumpCoroutine()
    {
        do
        {
            yield return new WaitForSeconds(0.02f);
        } 
        while (_rigidbody.velocity.y != 0);

        _isJumping = false;
        Physics.gravity = new Vector3(0, _normalGravity, 0);
    }
    
    public void StartLevel()
    {
        RoadGenerator.instance.StartLevel();
    }

    // Переместить в Singleton
    public void ResetGame()
    {
        _rigidbody.velocity = Vector3.zero;
        _startPoint = 0;
        _finishPoint = 0;
        transform.position = _startGamePosition;
        transform.rotation = _startGameRotation;
        RoadGenerator.instance.ResetGenerator();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NotLose")
        {
            MoveHorizontal(-_lastVectorX);
        }

        if (collision.gameObject.tag == "Lose")
        {
            SceneManager.LoadScene("Main/Scenes/Lost");
        }
    }
    
    
}
