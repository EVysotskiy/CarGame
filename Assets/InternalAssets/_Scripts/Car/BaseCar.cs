using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCar : MonoBehaviour, ICar
{
    private bool _isCrashed;
    private Transform _transform;
    public float speed
    {
        get => _speed;

        set
        {
            if (value >= 0)
            {
                _speed = value;
            }
        }
    }
    private float _speed;
    private IEnumerator _driveCoroutine;


    public BaseCar()
    {
        _driveCoroutine = DriveCoroutine();
    }
    public virtual void Drive()
    {
        if (_driveCoroutine != null)
        {
            StartCoroutine(_driveCoroutine);
        }
    }

    public void Stop()
    {
        StopCoroutine(_driveCoroutine);
    }
    
    public bool IsCrashed()
    {
        return _isCrashed;
    }

    private IEnumerator DriveCoroutine()
    {
        while (_isCrashed is false)
        {
            _transform.Translate(Vector3.forward * Time.deltaTime*speed);
            yield return null;
        }
    }
}
