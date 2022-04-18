using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InternalAssets._Scripts.Car
{
    public class CarView:BaseCarView
    {
        private List<Material> _materials;
        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;
        private void Awake()
        {
            Initialized();
        }
        
        private void Initialized()
        {
            _materials = new List<Material>();
            this._transform = transform;
            _meshRenderer = _transform.gameObject.GetComponent<MeshRenderer>();
            SetRanfomDriveType();
        }

        private void SetRanfomDriveType()
        {
            _driveType = Random.Range(0, 2) > 0
                ? DriveType.Passing
                : DriveType.Oncoming;
        }
        private void OnTriggerEnter(Collider other)
        {
            var roadPoint = other.transform.GetComponent<RoadPoint>();
            MoveInStartPoint(roadPoint);
            SetRandomMaterial();
            Debug.Log("OnTriggerEnter");
            
        }

        private void MoveInStartPoint(RoadPoint roadTriggerPoint)
        {
            if (roadTriggerPoint.GetDriveTypePoint() != _driveType)
            {
                return;
            }
            var newXCoordinate = roadTriggerPoint.GetTransformNextPoint().position.x;
            SetXCoordinate(newXCoordinate);
        }

        private void SetXCoordinate(float xCoordinate)
        {
            _transform.position = new Vector3(xCoordinate, _transform.position.y,_transform.position.z);
        }

        private Material GetRandomMaterial()
        {
            return _materials[Random.Range(0, _materials.Count)];
        }
        public override void SetRandomMaterial()
        {
            _meshRenderer.material = GetRandomMaterial();
        }

        public void SetMaterials(Material[] materials)
        {
            _materials.Clear();
            _materials.AddRange(materials);
        }
    }
}