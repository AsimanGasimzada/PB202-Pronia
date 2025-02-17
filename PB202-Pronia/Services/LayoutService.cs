using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PB202_Pronia.Contexts;
using PB202_Pronia.Models;
using PB202_Pronia.ViewModels.BasketViewModels;

namespace PB202_Pronia.Services;

public class LayoutService
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private const string BASKET_KEY = "ProniaBasket";

    public LayoutService(AppDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<BasketItem>> GetBasketAsync()
    {
        if (_contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user is null)
                throw new Exception("Bad request");

            var basketItems = await _context.BasketItems.Where(x => x.AppUserId == user.Id).Include(x => x.Product).ThenInclude(x=>x.ProductImages).ToListAsync();

            return basketItems;
        }
        else
        {
            var cookieValue = _contextAccessor.HttpContext?.Request.Cookies[BASKET_KEY];

            var basketVMList = new List<BasketItemVM>();
            if (cookieValue is not null)
            {
                basketVMList = JsonConvert.DeserializeObject<List<BasketItemVM>>(cookieValue) ?? [];
            }
            var basketItemList = new List<BasketItem>();


            foreach (var vm in basketVMList)
            {
                var product = await _context.Products.Include(x=>x.ProductImages).FirstOrDefaultAsync(x => x.Id == vm.ProductId);

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

            return basketItemList;
        }
    }
}
