using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс описывает поведение городского трафика
/// </summary>
public class BehaviourIntelegemceBotCar : MonoBehaviour
{
    private Transform transformCar;
    private float _lenghtRayBotCar = 40f;
    private Vector3 _vectorForward = new Vector3(0, 0, 1);
    private MeshRenderer _meshRenderer;
    public float _distanceCar = 4f;
    public TrafficAIController trafficAIController;

    /// <summary>
    /// Material car traffic
    /// </summary>
    private Material _materialMesh;


    /// <summary>
    /// Луч, исходящий из капота (передней части) автомобиля трафика
    /// </summary>
    private Ray _rayFrontCarBot;


    /// <summary>
    /// Размер коллайдера на автомобиле трафика
    /// по z
    /// </summary>
    private float sizeBoxColliderBotCar = 0.3f;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        SetMaterialCar(trafficAIController.GetRandomMaterialCar());
        transformCar = gameObject.transform;
    }


    private void SetMaterialCar(Material material) =>
        _meshRenderer.material = material;

    private void SetDistanceCar()
    {
        if (transformCar.position.z > 0)
        {
            _distanceCar = Random.Range(15f, _lenghtRayBotCar-10f);     
        }
        else
        {
            _distanceCar = Random.Range(9f, _lenghtRayBotCar-7f);
        }
    }

       

    /// <summary>
    /// Движение автомобиля
    /// </summary>
    private void DrivingCar()
    {
        transformCar.Translate(_vectorForward * Time.deltaTime* trafficAIController.speedCarTraffic);
        if(IsEndRoad())
        {
            SetCarStartPosition();
        }
    }


    private void SetCarStartPosition()
    {
        SetDistanceCar();
        StartPosition();
        LetRay();
        SetMaterialCar(trafficAIController.GetRandomMaterialCar());
    }

    private bool GetPassingDirectionCar() => 
        transformCar.position.z < 0 ? true : false;

    private void LetRay()
    {
        RaycastHit raycastHit = new RaycastHit();
        if (Physics.Raycast(GetRayCarBot(), out raycastHit))
        {
           
            if (GetPassingDirectionCar())
            {
                transformCar.position = raycastHit.transform.position - new Vector3(_distanceCar, 0, 0);
            }
            else
            {
                transformCar.position = raycastHit.transform.position + new Vector3(_distanceCar, 0, 0);
            }
        }
    }

    private bool IsEndRoad()
    {
        if ((transformCar.position.x < -30f && transformCar.position.z>0) || (transformCar.position.x > 130f && transformCar.position.z < 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<PlayerCar>() != null)
        {
            GameObject playerCar = collider.gameObject;
            if (playerCar.GetComponent<BoxCollider>().enabled)
            { 
                trafficAIController.CollidedCar(playerCar);
                playerCar.GetComponent<PlayerCar>().CollidedCar();
                Handheld.Vibrate();
            }
        }
    }
   
    
    /// <summary>
    /// Направление луча в зависимости от направления движения автомобиля трафика
    /// </summary>
    /// <param name="_comingGamingCar">Направление движения автомобиля трафика</param>
    /// <returns>Возвращает луч, направленный в сторону движения автомобиля трафика</returns>
    private Ray GetRayCarBot()
    {
        Vector3 lineStart;
        if (transformCar.position.z<0)
        {
             lineStart = new Vector3(transformCar.position.x + sizeBoxColliderBotCar, transformCar.position.y + 2f, transformCar.position.z); 
        }
        else
        {
             lineStart = new Vector3(transformCar.position.x - sizeBoxColliderBotCar, transformCar.position.y + 2f, transformCar.position.z);
        }
        return new Ray(lineStart, transformCar.forward * _lenghtRayBotCar);
    }
   
    private void StartPosition()
    {
        
        if (transformCar.position.x > 0f)
        {
            transformCar.position = transformCar.position - new Vector3(200, 0, 0);
        }
        else
        {
            transformCar.position = transformCar.position + new Vector3(200, 0, 0);           
        }
    }

    private void LateUpdate()
    {       
        DrivingCar();
    }
}


