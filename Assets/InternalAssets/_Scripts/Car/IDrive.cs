namespace InternalAssets._Scripts.Car
{
    public interface IDrive
    {
        void DestroyCar();
        void Drive();
        void SetSpeed(float speed);
        bool isDestroy { get; set; }
    }
}