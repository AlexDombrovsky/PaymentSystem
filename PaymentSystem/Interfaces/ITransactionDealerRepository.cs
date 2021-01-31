using System.Threading.Tasks;

namespace PaymentSystem.Interfaces
{
    public interface ITransactionDealerRepository
    {
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

        Task DisposeTransactionAsync();
    }
}