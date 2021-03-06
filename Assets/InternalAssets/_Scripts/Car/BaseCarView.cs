using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DriveType = InternalAssets._Scripts.Car.DriveType;

public abstract class BaseCarView : MonoBehaviour,ICar
{
    protected Transform _transform;
    protected DriveType _driveType;
    public float Speed { get => _speed; }
    private float _speed;
    private void Update()
    {
        _transform.Translate(Vector3.back * Time.deltaTime * _speed);
    }
    
    public void SetSpeed(float newSpeed)
    {
        if (newSpeed < 0)
        {
            throw new ArgumentException("Speed cannot be less than zero", name);
        }

        _speed = newSpeed;
    }
    
    public virtual void SetRandomMaterial()
    {
        throw new System.NotImplementedException();
    }
}
