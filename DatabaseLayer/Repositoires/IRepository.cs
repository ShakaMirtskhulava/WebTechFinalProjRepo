using DatabaseLayer.Data;
using DatabaseLayer.Repositories.ParameterEntities;
using Microsoft.Extensions.Logging;

namespace DatabaseLayer.Repositories;

public interface IRepository<RepoEntitiyType, KeyType> where RepoEntitiyType : class
{
    public AppDbContext dbContext { get; set; }
    public ILogger<IRepository<RepoEntitiyType, KeyType>> logger { get; set; }

    public abstract Task<IEnumerable<RepoEntitiyType>?> GetAllAsync();
    public abstract Task<RepoEntitiyType?> GetAsync(KeyType id);
    public abstract Task<RepoEntitiyType?> CreateAsync(IParameterEntity parameterEntity);
    public abstract Task<RepoEntitiyType?> UpdateAsync(IParameterEntity parameterEntity);
    public abstract Task<RepoEntitiyType?> RemoveAsync(KeyType id);
}
