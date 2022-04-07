using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ICar
{
    DriveType DriveType { get;}
    float Speed { get; }
    void SetRandomMaterial();
}
