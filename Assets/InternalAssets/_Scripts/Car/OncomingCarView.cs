using InternalAssets._Scripts.Car;
using UnityEngine;

public class OncomingCarView : CarView, IDrive
{
    public void Drive()
    {
        _transform.Translate(Vector3.back * Time.deltaTime * _speed);
    }

    public bool isDestroy { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroy)
        {
            Destroy(this);
        }
        
        var roadPoint = other.transform.GetComponent<RoadPoint>();
        if (roadPoint == null)
        {
            return;
        }
        MoveInStartPoint(roadPoint);
        SetRandomMaterial();
        Debug.Log("OnTriggerEnter");
            
    }
    private void MoveInStartPoint(RoadPoint roadTriggerPoint)
    {
        if (roadTriggerPoint.GetDriveTypePoint() != DriveType.Oncoming)
        {
            return;
        }
        var newXCoordinate = roadTriggerPoint.GetTransformNextPoint().position.x;
        SetXCoordinate(newXCoordinate);
    }
    
}

