using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB202_Pronia.Areas.Admin.ViewModels.ProductViewModels;
using PB202_Pronia.Contexts;
using PB202_Pronia.Models;
using PB202_Pronia.Services;

namespace PB202_Pronia.Areas.Admin.Controllers;
[Area("Admin")]
//[AutoValidateAntiforgeryToken]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ProductController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }



    public IActionResult Delete(int id)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == id);

        if(product == null) 
            return NotFound();  

        product.IsDeleted = true;
        _context.SaveChanges(); 

        return RedirectToAction("Index");
    }
    public IActionResult Index()
    {
        var products = _context.Products.IgnoreQueryFilters().Include(x => x.ProductImages).ToList();

        return View(products);
    }

    public IActionResult Create()
    {

        _addViewBagForProduct();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductCreateViewModel vm)
    {
        _addViewBagForProduct();

        if (!ModelState.IsValid)
            return View(vm);


        var isExistCategory = _context.Categories.Any(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "Category is not found");
            return View(vm);
        }



        foreach (var tagId in vm.TagIds)
        {
            var isExistTag = _context.Tags.Any(x => x.Id == tagId);

            if (!isExistTag)
            {
                ModelState.AddModelError("TagIds", "Tag is not found");
                return View(vm);
            }
        }

        if (!vm.MainImage.CheckTypes("image"))
        {
            ModelState.AddModelError("MainImage", "Please enter valid format");
            return View(vm);
        }
        if (!vm.MainImage.CheckSize(2))
        {
            ModelState.AddModelError("MainImage", "Image max size limit 2mb");
            return View(vm);
        }


        if (!vm.HoverImage.CheckTypes("image"))
        {
            ModelState.AddModelError("HoverImage", "Please enter valid format");
            return View(vm);
        }
        if (!vm.HoverImage.CheckSize(2))
        {
            ModelState.AddModelError("HoverImage", "Image max size limit 2mb");
            return View(vm);
        }


        foreach (var image in vm.AdditionalImages)
        {
            if (!image.CheckTypes("image"))
            {
                ModelState.AddModelError("AdditionalImages", "Please enter valid format");
                return View(vm);
            }
            if (!image.CheckSize(2))
            {
                ModelState.AddModelError("AdditionalImages", "Image max size limit 2mb");
                return View(vm);
            }
        }


        Product product = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            CategoryId = vm.CategoryId,
            Price = vm.Price,
            ProductTags = [],
            ProductImages = []
        };

        foreach (var tagId in vm.TagIds)
        {
            ProductTag productTag = new()
            {
                TagId = tagId,
                Product = product
            };

            product.ProductTags.Add(productTag);
        }


        string mainImagePath = vm.MainImage.CreateImage(_environment.WebRootPath, "assets", "images", "website-images");
        string hoverImagePath = vm.HoverImage.CreateImage(_environment.WebRootPath, "assets", "images", "website-images");

        ProductImage mainImage = new()
        {
            Product = product,
            Url = mainImagePath,
            Status = true
        };

        ProductImage hoverImage = new()
        {
            Product = product,
            Url = hoverImagePath,
            Status = false
        };

        product.ProductImages.Add(mainImage);
        product.ProductImages.Add(hoverImage);

        foreach (var image in vm.AdditionalImages)
        {
            string imagePath = image.CreateImage(_environment.WebRootPath, "assets", "images", "website-images");

            ProductImage newImage = new()
            {
                Product = product,
                Url = imagePath,
                Status = null
            };

            product.ProductImages.Add(newImage);
        }



        _context.Products.Add(product);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    private void _addViewBagForProduct()
    {
        var categories = _context.Categories.ToList();
        var tags = _context.Tags.ToList();

        ViewBag.Categories = categories;
        ViewBag.Tags = tags;
    }

    public IActionResult Detail(int id)
    {
        var product = _context.Products
            .Include(x => x.ProductImages).Include(x => x.Category).Include(x => x.ProductTags).ThenInclude(x => x.Tag)
            .FirstOrDefault(x => x.Id == id);

        if (product is null)
            return NotFound();

        return View(product);
    }

    public IActionResult Update(int id)
    {
        var product = _context.Products
         .Include(x => x.ProductImages).Include(x => x.Category).Include(x => x.ProductTags).ThenInclude(x => x.Tag)
         .FirstOrDefault(x => x.Id == id);

        if (product is null)
            return NotFound();

        _addViewBagForProduct();


        ProductUpdateViewModel vm = new()
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Price = product.Price,
            TagIds = product.ProductTags.Select(x => x.TagId).ToList(),
            ProductImages = product.ProductImages.Where(x => x.Status == null).ToList(),

        };

        return View(vm);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(ProductUpdateViewModel vm)
    {
        _addViewBagForProduct();

        var existProduct = _context.Products
         .Include(x => x.ProductImages).Include(x => x.Category).Include(x => x.ProductTags).ThenInclude(x => x.Tag)
         .FirstOrDefault(x => x.Id == vm.Id);

        if (existProduct is null)
            return NotFound();

        vm.ProductImages = existProduct.ProductImages.Where(x => x.Status == null).ToList();


        if (!ModelState.IsValid)
            return View(vm);

        var isExistCategory = _context.Categories.Any(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "Category is not found");
            return View(vm);
        }



        foreach (var tagId in vm.TagIds)
        {
            var isExistTag = _context.Tags.Any(x => x.Id == tagId);

            if (!isExistTag)
            {
                ModelState.AddModelError("TagIds", "Tag is not found");
                return View(vm);
            }
        }

        if (!vm.MainImage?.CheckTypes("image") ?? false)
        {
            ModelState.AddModelError("MainImage", "Please enter valid format");
            return View(vm);
        }
        if (!vm.MainImage?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("MainImage", "Image max size limit 2mb");
            return View(vm);
        }


        if (!vm.HoverImage?.CheckTypes("image") ?? false)
        {
            ModelState.AddModelError("HoverImage", "Please enter valid format");
            return View(vm);
        }
        if (!vm.HoverImage?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("HoverImage", "Image max size limit 2mb");
            return View(vm);
        }


        foreach (var image in vm.AdditionalImages)
        {
            if (!image.CheckTypes("image"))
            {
                ModelState.AddModelError("AdditionalImages", "Please enter valid format");
                return View(vm);
            }
            if (!image.CheckSize(2))
            {
                ModelState.AddModelError("AdditionalImages", "Image max size limit 2mb");
                return View(vm);
            }
        }



        existProduct.Name = vm.Name;
        existProduct.Description = vm.Description;
        existProduct.Price = vm.Price;
        existProduct.CategoryId = vm.CategoryId;

        //add new ProductTags
        foreach (var tagId in vm.TagIds)
        {
            var isExist = existProduct.ProductTags.Any(x => x.TagId == tagId);

            if (!isExist)
            {
                ProductTag tag = new()
                {
                    TagId = tagId,
                    ProductId = vm.Id
                };

                existProduct.ProductTags.Add(tag);
            }
        }

        //Remove old Product Tags

        foreach (var productTag in existProduct.ProductTags.ToList())
        {
            var isExist = vm.TagIds.Any(x => x == productTag.TagId);

            if (!isExist)
            {
                existProduct.ProductTags.Remove(productTag);
            }
        }



        if (vm.MainImage is not null)
        {
            string newMainImagePath = vm.MainImage.CreateImage(_environment.WebRootPath, "assets", "images", "website-images");

            var existMainImage = existProduct.ProductImages.FirstOrDefault(x => x.Status == true);
            if (existMainImage is not null)
            {
                FileService.FileDelete(existMainImage.Url, _environment.WebRootPath, "assets", "images", "website-images");

                existMainImage.Url = newMainImagePath;
            }
            else
            {
                ProductImage mainImage = new()
                {
                    ProductId = vm.Id,
                    Url = newMainImagePath,
                    Status = true
                };

                existProduct.ProductImages.Add(mainImage);
            }
        }




        if (vm.HoverImage is not null)
        {
            string newHoverImagePath = vm.HoverImage.CreateImage(_environment.WebRootPath, "assets", "images", "website-images");

            var existHoverImage = existProduct.ProductImages.FirstOrDefault(x => x.Status == false);
            if (existHoverImage is not null)
            {
                FileService.FileDelete(existHoverImage.Url, _environment.WebRootPath, "assets", "images", "website-images");
                existHoverImage.Url = newHoverImagePath;
            }
            else
            {
                ProductImage hoverImage = new()
                {
                    ProductId = vm.Id,
                    Url = newHoverImagePath,
                    Status = false
                };

                existProduct.ProductImages.Add(hoverImage);
            }
        }


        foreach (var image in vm.AdditionalImages)
        {
            string imagePath = image.CreateImage(_environment.WebRootPath, "assets", "images", "website-images");

            ProductImage newImage = new()
            {
                ProductId = vm.Id,
                Url = imagePath,
                Status = null
            };

            existProduct.ProductImages.Add(newImage);
        }



        _context.SaveChanges();

        return RedirectToAction("Index");


    }

    public IActionResult DeleteImage(int id)
    {
        var productImage = _context.ProductImages.FirstOrDefault(x => x.Id == id);

        if (productImage is null)
            return NotFound();

        _context.ProductImages.Remove(productImage);
        _context.SaveChanges();

        FileService.FileDelete(productImage.Url, _environment.WebRootPath, "assets", "images", "website-images");

        //string returnUrl=Request.Headers["Referer"];

        //if (string.IsNullOrEmpty(returnUrl))
        //    returnUrl = "/";

        //return Redirect(returnUrl )      ;

        return Ok();
    }
}
