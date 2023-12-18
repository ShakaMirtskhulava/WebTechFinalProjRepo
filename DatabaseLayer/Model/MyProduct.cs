using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Model;
public class MyProduct
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }

}
