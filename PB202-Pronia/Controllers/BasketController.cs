using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PB202_Pronia.Contexts;
using PB202_Pronia.Models;
using PB202_Pronia.ViewModels.BasketViewModels;

namespace PB202_Pronia.Controllers;

public class BasketController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private const string BASKET_KEY = "ProniaBasket";

    public BasketController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> AddToBasket(int id)
    {
        var isExistProduct = await _context.Products.AnyAsync(x => x.Id == id);

        if (!isExistProduct)
            return NotFound();

        if (User.Identity?.IsAuthenticated ?? false)
        {

            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return BadRequest();

            var existBasketItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == id && x.AppUserId == user.Id);


            if (existBasketItem is { }) // is not null
            {
                existBasketItem.Count++;

                await _context.SaveChangesAsync();

                return PartialView("_BasketModalPartial");
            }

            BasketItem basketItem = new()
            {
                ProductId = id,
                AppUserId = user.Id,
                Count = 1
            };

            await _context.BasketItems.AddAsync(basketItem);
            await _context.SaveChangesAsync();

        }
        else
        {

            List<BasketItemVM> basketItems = [];

            var cookieValue = Request.Cookies[BASKET_KEY];

            if (cookieValue is not null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(cookieValue) ?? [];
            }

            var existItem = basketItems.FirstOrDefault(x => x.ProductId == id);

            if (existItem is { })
            {
                existItem.Count++;
            }
            else
            {
                BasketItemVM basketItemVM = new()
                {
                    ProductId = id,
                    Count = 1
                };

                basketItems.Add(basketItemVM);
            }

            string json = JsonConvert.SerializeObject(basketItems);

            Response.Cookies.Append(BASKET_KEY, json);


        }

        return PartialView("_BasketModalPartial");

    }


    public async Task<IActionResult> GetBasket()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return BadRequest();

            var basketItems = await _context.BasketItems.Where(x => x.AppUserId == user.Id).Include(x => x.Product).ToListAsync();

            return Json(basketItems);
        }
        else
        {
            var cookieValue = Request.Cookies[BASKET_KEY];

            var basketVMList = new List<BasketItemVM>();
            if (cookieValue is not null)
            {
                basketVMList = JsonConvert.DeserializeObject<List<BasketItemVM>>(cookieValue) ?? [];
            }

            var basketItemList = new List<BasketItem>();


            foreach (var vm in basketVMList)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == vm.ProductId);

                if (product is { })
                {
                    BasketItem newBasketItem = new()
                    {
                        ProductId = vm.ProductId,
                        Product = product,
                        Count = vm.Count,
                    };

                    basketItemList.Add(newBasketItem);

                }
            }

            return Json(basketItemList);
        }
    }

    public async Task<IActionResult> RemoveToBasket(int id)
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return BadRequest();

            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.ProductId == id);

            if (basketItem is null)
                return NotFound();

            _context.BasketItems.Remove(basketItem);
            await _context.SaveChangesAsync();
        }
        else
        {
            List<BasketItemVM> basketItems = [];

            var cookieValue = Request.Cookies[BASKET_KEY];

            if (cookieValue is not null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(cookieValue) ?? [];
            }

            var existItem = basketItems.FirstOrDefault(x => x.ProductId == id);

            if (existItem is null)
                return NotFound();

            basketItems.Remove(existItem);

            var json = JsonConvert.SerializeObject(basketItems);

            Response.Cookies.Append(BASKET_KEY, json);
        }

        string? returnUrl=Request.Headers["Referer"];

        if (returnUrl is null)
            returnUrl = "/";

        return Redirect(returnUrl);
    }
}
