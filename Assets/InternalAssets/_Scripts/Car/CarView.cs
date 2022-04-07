using System;
using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets._Scripts.Car
{
    public class CarView:BaseCarView
    {
        private List<Material> _materials;
        private MeshRenderer _meshRenderer;
        private void Awake()
        {
            Initialized();
        }

        private void Initialized()
        {
            this._transform = transform;
            _meshRenderer = _transform.gameObject.GetComponent<MeshRenderer>();
        }

        public void SetMaterials(Material[] materials)
        {
            _materials.Clear();
            _materials.AddRange(materials);
        }
    }
}