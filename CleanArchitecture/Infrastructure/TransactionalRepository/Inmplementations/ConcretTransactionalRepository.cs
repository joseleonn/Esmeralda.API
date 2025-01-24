using Infrastructure.TransactionalRepository.Inmplementations;
using Infrastructure.TransactionalRepository.Interfaces;

public class ConcreteTransactionalRepository : TransactionalRepository
{
    public ConcreteTransactionalRepository(ITransactionalConfiguration configuration)
        : base(configuration)
    {
    }
}