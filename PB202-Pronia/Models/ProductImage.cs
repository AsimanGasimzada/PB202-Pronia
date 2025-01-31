namespace PB202_Pronia.Models;

public class ProductImage : BaseEntity
{
    public string Url { get; set; }
    public bool? Status { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
}