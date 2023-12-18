using DatabaseLayer.Model;

namespace DatabaseLayer.Repositories;

public interface IProductRepository : IRepository<MyProduct, int>
{
}
