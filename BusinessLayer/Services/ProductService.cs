using DatabaseLayer;
using DatabaseLayer.Model;
using DatabaseLayer.Repositories.ParameterEntities;

namespace BusinessLayer.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;


    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<MyProduct?> AddNewProductAsync(MyProduct myProduct)
    {
        var createProductParameterEntity = new CreateProductParameterEntity()
        {
            Name = myProduct.Name,
            Description = myProduct.Description,
            Price = myProduct.Price
        };

        var newProduct = await _unitOfWork.ProductRepository.CreateAsync(createProductParameterEntity);
        await _unitOfWork.SaveChangesAsync();
        return newProduct;
    }

    public async Task<IEnumerable<MyProduct>?> GetAllProductsAsync()
    {
        return await _unitOfWork.ProductRepository.GetAllAsync();
    }

    public async Task<MyProduct?> UpdateProductAsync(MyProduct myProduct)
    {
        var updateProductParameterEntity = new UpdateProductParameterEntity()
        {
            Id = myProduct.ProductId,
            Name = myProduct.Name,
            Description = myProduct.Description,
            Price = myProduct.Price
        };

        var updatedProduct = await _unitOfWork.ProductRepository.UpdateAsync(updateProductParameterEntity);
        await _unitOfWork.SaveChangesAsync();
        return updatedProduct;
    }
    public async Task<MyProduct?> DeleteProductAsync(int id)
    {
        var deletedProduct = await _unitOfWork.ProductRepository.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return deletedProduct;
    }
    public async Task<MyProduct?> GetProductByIdAsync(int id)
    {
        return await _unitOfWork.ProductRepository.GetAsync(id);
    }

}
