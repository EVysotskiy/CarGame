using System;
using System.Collections.Generic;
using UnityEngine;

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
            this._transform = transform;
            _meshRenderer = _transform.gameObject.GetComponent<MeshRenderer>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter");
        }

        public void SetMaterials(Material[] materials)
        {
            _materials.Clear();
            _materials.AddRange(materials);
        }
    }
}