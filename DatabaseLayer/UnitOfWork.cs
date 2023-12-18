using DatabaseLayer.Data;
using DatabaseLayer.Repositories;

namespace DatabaseLayer;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    public IProductRepository ProductRepository { get; set; }


    public UnitOfWork(AppDbContext context, IProductRepository productRepository)
    {
        _context = context;
        ProductRepository = productRepository;
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}
