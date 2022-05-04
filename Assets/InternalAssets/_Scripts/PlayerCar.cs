using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;
    private MeshRenderer _meshRenderer;
    public float speedCar { get; set; }
    private ParticleSystem _particleSystem;
    private Transform _transform;
    private Vector3 _vector3Forward = new Vector3(0, 0, -1);
    public bool isColision = false;
    private void Awake()
    {
        _particleSystem = this.GetComponentInChildren<ParticleSystem>();
        _meshRenderer = this.GetComponent<MeshRenderer>();
        SetRandomMaterial();
    }

    private Material GetRandomMaterial()
    {
        return _materials[Random.Range(0, _materials.Count)];
    }
    private void SetRandomMaterial()
    {
        _meshRenderer.material = GetRandomMaterial();
    }

    private void ShowExplosion()
    {
        _particleSystem.Play();
    }

    
    public void AnimationTurnExit()
    {
        if (isColision) return;
        _transform = gameObject.transform; 
        _transform.GetComponent<Animator>().enabled = false;
        _transform.GetComponent<BoxCollider>().enabled = false;
        Destroy(_transform.GetComponent<Rigidbody>());
        GameController.Instance.OnEndTurn();
        GameController.Instance.OnDestroyPlayerCar(gameObject);
        DestroyPlayerCar();
    }

    private void DestroyPlayerCar()
    {
        Destroy(gameObject.GetComponent<PlayerCar>());
    }

    private void OnTriggerEnter(Collider other)
    {
        CollidedCar();
    }

    public void CollidedCar()
    {
        if (transform.GetComponent<BoxCollider>().enabled)
        {
            isColision = true;
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetComponent<Animator>().SetFloat("SpeedAnimation", 0);
            ShowExplosion();
            GameController.Instance.OnCrashed();
        }
    }
    
}
