using DatabaseLayer.Configuration;
using DatabaseLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DatabaseLayer.Data;

public class AppDbContext : DbContext
{
    private readonly DatabaseConfigurationSettings _databaseConfigurationSettings;
    public DbSet<MyProduct> Products { get; set; }

    public AppDbContext(IOptions<DatabaseConfigurationSettings> databaseConfigurationSettings)
    {
        _databaseConfigurationSettings = databaseConfigurationSettings.Value;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if(_databaseConfigurationSettings is null)
            throw new ArgumentNullException(nameof(_databaseConfigurationSettings));

        optionsBuilder.UseSqlServer(_databaseConfigurationSettings.ConnectionString);
    }

}
