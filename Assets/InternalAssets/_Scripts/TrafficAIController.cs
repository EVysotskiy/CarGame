using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrafficAIController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
   [Tooltip("Скорость потока")] [Range(0f,50f)] public float speedCarTraffic;
    public List<Material> materialsCarsTraffic;


    public Material GetRandomMaterialCar() => 
        materialsCarsTraffic[Random.Range(0, materialsCarsTraffic.Count)];

    public void UpSpeed() => 
        SpeedTraffic.UpSpeed(ref speedCarTraffic, 0.5f);

    public void ResetSpeedNull() =>
        SpeedTraffic.SetSpeed(ref speedCarTraffic, 0f);

    public void ResetSpeed() =>
    SpeedTraffic.SetSpeed(ref speedCarTraffic, 10f);

    public void CollidedCar(GameObject playerCar)
    {
        speedCarTraffic = 0;
        _gameController.SetColiderCarPlayer(ref playerCar);
        _gameController.LoseGame();

    } 
}
static class SpeedTraffic
{
    public static void UpSpeed(ref float speed, float value)
    {
        speed += value;
    }

    public static void DownSpeed(ref float speed, float value)
    {
        speed -= value;
    }

    public static void SetSpeed(ref float speed, float value)
    {
        speed = value;
    }
}