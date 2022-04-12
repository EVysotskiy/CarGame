using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
    }
}
