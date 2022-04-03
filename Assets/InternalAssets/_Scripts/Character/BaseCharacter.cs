using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, ICharacter
{
    public virtual void Run()
    {
        throw new System.NotImplementedException();
    }
    
    public virtual void Stand()
    {
        throw new System.NotImplementedException();
    }
}
