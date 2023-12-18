using DatabaseLayer.Repositories;

namespace DatabaseLayer;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; set; }
    Task SaveChangesAsync();
}
