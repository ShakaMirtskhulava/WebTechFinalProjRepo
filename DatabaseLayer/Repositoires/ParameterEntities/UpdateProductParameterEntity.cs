namespace DatabaseLayer.Repositories.ParameterEntities;

public class UpdateProductParameterEntity : IParameterEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
