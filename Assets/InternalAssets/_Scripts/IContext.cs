using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContext
{
    MonoBehaviour Current { get;}
    GameObject InstanceView(string prefabName);
}