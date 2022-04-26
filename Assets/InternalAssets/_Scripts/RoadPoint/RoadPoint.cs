using System.Collections;
using System.Collections.Generic;
using InternalAssets._Scripts.Car;
using UnityEngine;

public class RoadPoint : MonoBehaviour
{
    [SerializeField]
    private RoadPoint _nextPoint;
    [SerializeField]
    private DriveType _driveType;

    public Transform GetTransformNextPoint() => _nextPoint.transform;
    public DriveType GetDriveTypePoint() => _driveType;
}