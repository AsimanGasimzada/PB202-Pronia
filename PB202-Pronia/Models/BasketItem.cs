namespace PB202_Pronia.Models;

public class BasketItem : BaseEntity
{
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public AppUser AppUser { get; set; }
    public string AppUserId { get; set; }
    public int Count { get; set; }
}
