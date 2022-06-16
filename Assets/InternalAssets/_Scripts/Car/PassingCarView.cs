using InternalAssets._Scripts.Car;
using UnityEngine;

public class PassingCarView : CarView,IDrive
{
    
    public void DestroyCar()
    {
        if (this != null)
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
    public void Drive()
    {
        if (_transform == null)
        {
            return;
        }
        _transform.Translate(Vector3.back * Time.deltaTime * _speed);
    }

    public bool isDestroy { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroy)
        {
            Destroy(gameObject);
        }
        
        var roadPoint = other.transform.GetComponent<RoadPoint>();
        if (roadPoint == null)
        {
            return;
        }
        MoveInStartPoint(roadPoint);
        SetRandomMaterial();
    }
    
    private void MoveInStartPoint(RoadPoint roadTriggerPoint)
    {
        if (roadTriggerPoint.GetDriveTypePoint() != DriveType.Passing)
        {
            return;
        }
        var newXCoordinate = roadTriggerPoint.GetTransformNextPoint().position.x;
        SetXCoordinate(newXCoordinate);
    }
}
