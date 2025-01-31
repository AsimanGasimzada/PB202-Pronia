using PB202_Pronia.Models;

namespace PB202_Pronia.Areas.Admin.ViewModels.ProductViewModels;

public class ProductUpdateViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public IFormFile? MainImage { get; set; }
    public IFormFile? HoverImage { get; set; }
    public List<IFormFile> AdditionalImages { get; set; } = [];
    public List<ProductImage>? ProductImages { get; set; }
    public List<int> TagIds { get; set; }
}
