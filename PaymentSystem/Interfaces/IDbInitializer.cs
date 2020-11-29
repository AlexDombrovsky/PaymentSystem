namespace PaymentSystem.Interfaces
{
    public interface IDbInitializer
    {
        void Initialize();

        void SeedData();
    }
}