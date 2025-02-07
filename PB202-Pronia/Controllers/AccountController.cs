using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB202_Pronia.Models;
using PB202_Pronia.ViewModels;

namespace PB202_Pronia.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var user = await _userManager.FindByEmailAsync(vm.EmailOrUsername);

        if (user is null)
        {
            user = await _userManager.FindByNameAsync(vm.EmailOrUsername);

            if (user is null)
            {
                ModelState.AddModelError("", "Invalid Username/Email or password ");
                return View(vm);
            }
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, vm.Password, true, true);

        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Invalid Username/Email or password ");
            return View(vm);
        }

        return RedirectToAction("Index", "Home");

    }


    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var isExistUser = await _userManager.Users.AnyAsync(x => x.NormalizedEmail == vm.Email.ToUpper());

        if (isExistUser)
        {
            ModelState.AddModelError("", "this email is already exist");
            return View(vm);
        }

        isExistUser = await _userManager.Users.AnyAsync(x => x.NormalizedUserName == vm.Username.ToUpper());

        if (isExistUser)
        {
            ModelState.AddModelError("", "this username is already exist");
            return View(vm);
        }


        AppUser user = new()
        {
            UserName = vm.Username,
            Email = vm.Email
        };

        var result = await _userManager.CreateAsync(user, vm.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(vm);
        }

        await _signInManager.SignInAsync(user,false);

        return RedirectToAction("Index","Home");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index","Home");
    }
}
