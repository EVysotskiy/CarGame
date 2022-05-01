using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets._Scripts.Car
{
    public class CarController : Window<CarView>
    {
        private List<CarData> _carDatas;
        private List<CarView> _carViews;
        private const string NAME_CAR_DATA_RESOURCES = "car/carDate/Car";
        public CarController(IContext context) : base(context)
        {
            InitialezeCarDatas();
            InitializedSpawnCar();
        }

        private void InitialezeCarDatas()
        {
            var carData = GetCarResourcesByName(NAME_CAR_DATA_RESOURCES);
            _carDatas.Add(carData);
        }
        private void InitializedSpawnCar()
        {
            _carViews = new List<CarView>();
            _context.Current.StartCoroutine(SpawnCar());
        }
        
        private IEnumerator SpawnCar()
        {
            for (int i = 0; i < 4; i++)
            {
                var newCar = _carDatas[Random.Range(0, _carDatas.Count)].GetNewCar();
                newCar.SetSpeed(10f);
                _carViews.Add(newCar);
                yield return new WaitForSeconds(1.5f);
            }
        }
    }
}