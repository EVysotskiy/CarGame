using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    private TouchControlls _touchControlls;
    public delegate void TouchScreen(InputAction.CallbackContext callbackContext);

    private void Awake()
    {
        _touchControlls = new TouchControlls();
    }

    private void OnEnable()
    {
        _touchControlls.Enable();
    }

    public void SubscribeTouchScreen(TouchScreen onTouchScreen)
    {
        if (_touchControlls == null)
        {
            _touchControlls = new TouchControlls();
        }
        _touchControlls.Touch.TouchInput.started += context => onTouchScreen(context);
    }

    public void UnsubscribeTouchScreen(TouchScreen onTouchScreen)
    {
        if (_touchControlls == null)
        {
           return;
        }
        _touchControlls.Touch.TouchInput.started -= context => onTouchScreen(context);
    }
    private void OnDisable()
    {
        _touchControlls.Disable();
    }
}
