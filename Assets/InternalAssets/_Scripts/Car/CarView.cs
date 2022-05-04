using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InternalAssets._Scripts.Car
{
    public abstract class CarView:BaseCarView
    {
        private List<Material> _materials;
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
        }


        protected void SetXCoordinate(float xCoordinate)
        {
            _transform.position = new Vector3(xCoordinate, _transform.position.y,_transform.position.z);
        }

        private Material GetRandomMaterial()
        {
            if (_materials == null || _materials.Count < 1)
            {
                return null;
            }
            return _materials[Random.Range(0, _materials.Count)];
        }
        public override void SetRandomMaterial()
        {
            var randomMaterial = GetRandomMaterial();
            if (randomMaterial == null)
            {
                return;
            }
            _meshRenderer.material = randomMaterial;
        }

        public void SetMaterials(Material[] materials)
        {
            _materials.Clear();
            _materials.AddRange(materials);
        }
    }
}