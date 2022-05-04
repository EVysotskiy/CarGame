using InternalAssets._Scripts.Car;
using UnityEngine;
[CreateAssetMenu(fileName = "New car", menuName = "Car Data", order = 51)]
public class CarData : ScriptableObject
{
   [SerializeField] private Material[] _materials;
   [SerializeField] private GameObject _prefabPassing;
   [SerializeField] private GameObject _prefabOncoming;

   public IDrive GetNewCar()
   {
      var driveType = GetRandomDriveType();
      var newCar = GetIDriveByDriveType(driveType);
      return newCar;
   }
   
   private DriveType GetRandomDriveType()
   {
     return Random.Range(-1, 3) > 0
         ? DriveType.Passing
         : DriveType.Oncoming;
   }

   private IDrive GetIDriveByDriveType(DriveType driveType)
   {
      var car = driveType is DriveType.Oncoming ? Instantiate(_prefabOncoming) : Instantiate(_prefabPassing);
      if (driveType is DriveType.Oncoming)
      {
         var oncomingCarView = car.GetComponent<OncomingCarView>();
         oncomingCarView.SetMaterials(_materials);
         return oncomingCarView;
      }
      var passingCarView = car.GetComponent<PassingCarView>();
      passingCarView.SetMaterials(_materials);
      return passingCarView;
   }
   
   
   
}
