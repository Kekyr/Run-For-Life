using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static bool Tap;
    public static bool SwipeLeft;
    public static bool SwipeRight;
    public static bool SwipeUp;
    public static bool SwipeDown;
    
    private bool _isDragging = false;
    private Vector2 _touchStartPosition;
    private Vector2 _swipeDistance;


    private void Update()
    {
        Tap  = false;
        SwipeDown = false;
        SwipeLeft = false;
        SwipeRight = false;
        SwipeUp = false;
        
        // ПК-версия
        CheckMouseInput();
        
        // Мобильная версия
        CheckTouchInput();
    
        // Просчитать дистанцию
        CalculateSwipeDistance();

        // Проверка на пройденность расстояния
        CheckSwipeDistance();

    }
    
    private void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tap = true;
            _isDragging = true;
            _touchStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            Reset();
        }
    }

    private void CheckTouchInput()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Tap = true;
                _isDragging = true;
                _touchStartPosition = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDragging = false;
                Reset();
            }
        }
    }
    
    private void CalculateSwipeDistance()
    {
        _swipeDistance = Vector2.zero;

        if (_isDragging)
        {
            if (Input.touches.Length < 0)
            {
                _swipeDistance = Input.touches[0].position - _touchStartPosition;
            }
            else if (Input.GetMouseButton(0))
            {
                _swipeDistance = (Vector2) Input.mousePosition - _touchStartPosition;
            }
        }
    }
    
    private void CheckSwipeDistance()
    {
        if (_swipeDistance.magnitude > 100)
        {
            // Определение направления
            float x = _swipeDistance.x;
            float y = _swipeDistance.y;

            CheckSwipeDirection(x, y);

            Reset();
        }
    }

    private static void CheckSwipeDirection(float x, float y)
    {
        if ((Mathf.Abs(x)) > (Mathf.Abs(y)))
        {
            if (x < 0)
            {
                SwipeLeft = true;
            }
            else
            {
                SwipeRight = true;
            }
        }
        else
        {
            if (y < 0)
            {
                SwipeDown = true;
            }
            else
            {
                SwipeUp = true;
            }
        }
    }

    private void Reset()
    {
        _touchStartPosition = Vector2.zero;
        _swipeDistance = Vector2.zero;
        _isDragging = false;
    }
}
