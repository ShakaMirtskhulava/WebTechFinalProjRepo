using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels;

#nullable disable
public class MyProductVM
{
    public int ProductId { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }
    [RegularExpression(@"^[0-9]+([.,][0-9]{1,2})?$", ErrorMessage = "Please specify the valid price")]
    public string Price { get; set; }
}
