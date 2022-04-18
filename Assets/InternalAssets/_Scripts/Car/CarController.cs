using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets._Scripts.Car
{
    public class CarController : Window<CarView>
    {
        private List<CarData> _carDatas;
        private List<CarView> _carViews;
        public CarController(IContext context,List<CarData> carDatas) : base(context)
        {
            _carDatas = new List<CarData>(carDatas);
            Initialized();
        }

        private void Initialized()
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