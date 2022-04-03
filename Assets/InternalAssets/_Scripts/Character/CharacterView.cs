using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public sealed class CharacterView : BaseCharacter
{
    private Animator _animator;
    private const string IS_RUN_PARAMETR_ANIMATOR = "isRun";
    private Coroutine _movementCoroutine;
    private Transform _transform;
    private float _speed = 0.03f;

    private void Awake()
    {
        Initialize();
        RandomPosition();
    }

    private void RandomPosition()
    {
        _transform.position = new Vector3(transform.position.x + Random.Range(-4, 4), transform.position.y,
            transform.position.z + Random.Range(-4, 4));
    }
    private void Initialize()
    {
        _animator = this.GetComponent<Animator>();
        _transform = this.transform;
    }

    public override void Run()
    {
        StartAnimationRun();
        _movementCoroutine = StartCoroutine(Movement());
        StartAnimationRun();
    }

    public override void Stand()
    {
        if (_movementCoroutine == null)
        {
            Debug.LogError($"<b><color=#FF0000>Null ref _movementCoroutine.</color></b>",this);
            return;
        }
        StopCoroutine(_movementCoroutine);
        StopAnimationRun();
    }

    private void StartAnimationRun()
    {
        SetStateAnimationRun(true);
    }

    private void StopAnimationRun()
    {
        SetStateAnimationRun(false);
    }

    private void SetStateAnimationRun(bool isRun)
    {
        _animator.SetBool(IS_RUN_PARAMETR_ANIMATOR,isRun);
    }
    
    private IEnumerator Movement()
    {
        while (true)
        {
            _transform.Translate(Vector3.forward * _speed);
            yield return null;
        }
    }
    
}
