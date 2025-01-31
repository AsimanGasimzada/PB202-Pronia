namespace PB202_Pronia.Models;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<ProductTag> ProductTags { get; set; } = [];
}
