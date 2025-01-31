namespace PB202_Pronia.Areas.Admin.ViewModels.ProductViewModels;

public class ProductCreateViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public IFormFile MainImage { get; set; } = null!;
    public IFormFile HoverImage { get; set; } = null!;
    public List<IFormFile> AdditionalImages { get; set; } = [];
    public List<int> TagIds { get; set; }
}
