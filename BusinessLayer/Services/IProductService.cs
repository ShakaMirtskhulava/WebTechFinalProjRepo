using DatabaseLayer.Model;

namespace BusinessLayer.Services;

public interface IProductService
{
    Task<IEnumerable<MyProduct>?> GetAllProductsAsync();
    Task<MyProduct?> AddNewProductAsync(MyProduct myProduct);
    Task<MyProduct?> UpdateProductAsync(MyProduct myProduct);
    Task<MyProduct?> DeleteProductAsync(int id);
    Task<MyProduct?> GetProductByIdAsync(int id);
}
