using System.ComponentModel.DataAnnotations.Schema;

namespace PB202_Pronia.Models;
public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public List<ProductImage> ProductImages { get; set; } = [];
    public List<ProductTag> ProductTags { get; set; } = [];
    public bool IsDeleted { get; set; } = false;

}
