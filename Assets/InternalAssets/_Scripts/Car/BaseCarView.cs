using System;
using UnityEngine;
using DriveType = InternalAssets._Scripts.Car.DriveType;

public abstract class BaseCarView : MonoBehaviour,ICar
{
    protected Transform _transform;
    protected DriveType _driveType;
    public float Speed { get => _speed; }

    protected float _speed;
    
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
