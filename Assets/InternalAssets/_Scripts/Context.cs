using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour,IContext
{
    public MonoBehaviour Current { get;}

    public Context(MonoBehaviour monoBehaviour)
    {
        Current = monoBehaviour;
    }
    
    public GameObject InstanceView(string prefabName)
    {
        return Instantiate(PrefabManager.PrefabManager.GetPrefabByName(prefabName));
    }
    
    
}
