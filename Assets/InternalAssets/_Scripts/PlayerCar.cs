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
        if (gameObject.GetComponent<BoxCollider>().enabled)
        {
            _transform = gameObject.transform;
            StartCoroutine(DravingCar());
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
   
    public void CollidedCar()
    {
        GameObject playerCar = gameObject;
        playerCar.GetComponent<BoxCollider>().enabled = false;
        playerCar.GetComponent<Animator>().SetFloat("SpeedAnimation", 0);
        ShowExplosion();
    }

    private IEnumerator  DravingCar()
    {
        while (true)
        {
            _transform.Translate(_vector3Forward * Time.deltaTime* speedCar);
            if (_transform.position.x > 120f || _transform.position.x < -30f)
            {
                Destroy(gameObject);
            }
            yield return new WaitForFixedUpdate();
        }
       
    }
}
