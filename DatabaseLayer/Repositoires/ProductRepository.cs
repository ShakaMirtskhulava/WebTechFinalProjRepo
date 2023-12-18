using DatabaseLayer.Data;
using DatabaseLayer.Model;
using DatabaseLayer.Repositories.ParameterEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatabaseLayer.Repositories;

public class ProductRepository : IProductRepository
{
    public AppDbContext dbContext { get; set; }
    public ILogger<IRepository<MyProduct, int>> logger { get; set; }


    public ProductRepository(AppDbContext Context, ILogger<IRepository<MyProduct, int>> Logger)
    {
        dbContext = Context;
        logger = Logger;
    }


    public async Task<MyProduct?> CreateAsync(IParameterEntity parameterEntity)
    {
        if(parameterEntity is CreateProductParameterEntity productParameterEntity)
        {
            var product = new MyProduct()
            {
                Name = productParameterEntity.Name,
                Price = productParameterEntity.Price,
                Description = productParameterEntity.Description
            };

            var newProduct = await dbContext.Products.AddAsync(product);
            logger.BeginScope("ProductRepository.CreateAsync: newProduct.Entity = {newProduct.Entity}", newProduct.Entity);
            return newProduct.Entity;
        }

        logger.LogError("ProductRepository.CreateAsync: parameterEntity is not of type CreateProductParameterEntity");
        return null;
    }
    public async Task<IEnumerable<MyProduct>?> GetAllAsync()
    {
        var product = await dbContext.Products.ToListAsync();
        return product;
    }

    public async Task<MyProduct?> GetAsync(int id)
    {
        return await dbContext.Products.FirstOrDefaultAsync(pr => pr.ProductId == id);
    }


    public async Task<MyProduct?> UpdateAsync(IParameterEntity parameterEntity)
    {
        if (parameterEntity is UpdateProductParameterEntity putProductParameterEntity)
        {
            var targetProduct = await dbContext.Products.FirstOrDefaultAsync(pr => pr.ProductId == putProductParameterEntity.Id);
            if (targetProduct == null)
                return null;

            targetProduct.Name = putProductParameterEntity.Name;
            targetProduct.Description = putProductParameterEntity.Description;
            targetProduct.Price = putProductParameterEntity.Price;

            var updatedProduct = dbContext.Products.Update(targetProduct);
            logger.BeginScope("ProductRepository.UpdateAsync: newProduct.Entity = {newProduct.Entity}", updatedProduct.Entity);
            return await Task.FromResult(updatedProduct.Entity);
        }
        return null;
    }

    public async Task<MyProduct?> RemoveAsync(int id)
    {
        var targetProduct = await dbContext.Products.FirstOrDefaultAsync(pr => pr.ProductId == id);
        if (targetProduct == null)
        {
            logger.LogError("ProductRepository.RemoveAsync: targetProduct is null");
            return null;
        }
        var removedProduct = dbContext.Remove(targetProduct);
        
        logger.BeginScope("ProductRepository.RemoveAsync: removedProduct.Entity = {removedProduct.Entity}", removedProduct.Entity);
        return await Task.FromResult(removedProduct.Entity);
    }
}
