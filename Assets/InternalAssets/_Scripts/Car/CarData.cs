using System.Collections;
using System.Collections.Generic;
using InternalAssets._Scripts.Car;
using UnityEngine;
[CreateAssetMenu(fileName = "New car", menuName = "Car Data", order = 51)]
public class CarData : ScriptableObject
{
   [SerializeField] private Material[] _materials;
   [SerializeField] private GameObject _prefab;

   public CarView GetNewCar()
   {
      var newCar = Instantiate(_prefab);
      newCar.AddComponent<CarView>();
      return newCar.GetComponent<CarView>();
   }
}
