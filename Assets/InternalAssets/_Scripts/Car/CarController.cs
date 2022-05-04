using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets._Scripts.Car
{
    public class CarController : Window<CarView>
    {
        private List<CarData> _carDatas;
        private const float START_SPEED_CARS = 10f; 
        private const float INCREMENT_SPEED_CARS = 0.5f;
        private List<CarView> _carViews;
        private List<IDrive> _drivesCar;
        private Coroutine _driveCoroutine;
        private const string NAME_CAR_DATA_RESOURCES = "car/carData/Car";
        private float _currentSpeedCars = 0f;
        public CarController(IContext context) : base(context)
        {
            _currentSpeedCars = 10f;
            InitialezeCarDatas();
            InitializedSpawnCar();
            DriveCars();
        }

        private void DriveCars()
        {
            _driveCoroutine = _context.Current.StartCoroutine(DriveCarEnumerator());
        }

        public void AddCarByGameobject(IDrive car)
        {
            car.isDestroy = true;
            _drivesCar.Add(car);
            StartCars();
        }

        public void StopCars()
        {
            _drivesCar.ForEach(carDrive =>
            {
                carDrive.SetSpeed(0f);
            });
        }

        public void IncrementSpeed()
        {
            _currentSpeedCars += INCREMENT_SPEED_CARS;
            StartCars();
        }


        public void SetStartSpeed()
        {
            SetSpeed(START_SPEED_CARS);
        }
        
        public void StartCars()
        {
            _drivesCar.ForEach(carDrive =>
            {
                carDrive.SetSpeed(_currentSpeedCars);
            });
        }
        public void SetSpeed(float newSpeed)
        {
            _drivesCar.ForEach(carDrive =>
            {
                carDrive.SetSpeed(newSpeed);
            });
        }
        private void InitialezeCarDatas()
        {
            _carDatas = new List<CarData>();
            var carData = GetCarResourcesByName(NAME_CAR_DATA_RESOURCES);
            _carDatas.Add(carData);
        }
        private void InitializedSpawnCar()
        {
            _drivesCar = new List<IDrive>();
            _context.Current.StartCoroutine(SpawnCar());
        }


        private IEnumerator DriveCarEnumerator()
        {
            while (true)   
            {
                _drivesCar.ForEach(car =>
                {
                    car.Drive();
                });
                yield return null;
            }
        }
        private IEnumerator SpawnCar()
        {
            for (int i = 0; i < 4; i++)
            {
                var newCar = _carDatas[Random.Range(0, _carDatas.Count)].GetNewCar();
                newCar.SetSpeed(_currentSpeedCars);
                _drivesCar.Add(newCar);
                yield return new WaitForSeconds(1.5f);
            }
        }
    }
}