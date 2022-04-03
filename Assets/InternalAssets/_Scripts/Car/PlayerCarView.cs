using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarView : BaseCar
{
    private Animator _animator;

    private void Awake()
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override void Drive()
    {
        
    }

    public void Turn()
    {
        
    }

    public void Crashed()
    {
        
    }
}
