namespace InternalAssets._Scripts.Car
{
    public interface IDrive
    {
        void Drive();
        void SetSpeed(float speed);
        bool isDestroy { get; set; }
    }
}