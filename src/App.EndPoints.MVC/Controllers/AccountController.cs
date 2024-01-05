using App.EndPoints.MVC.Models;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Controllers;
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AccountController(SignInManager<User> signInManager
            , UserManager<User> userManager
        , RoleManager<Role> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl)
    {


        LoginViewModel model = new()
        {
            ReturnUrl = returnUrl
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (isPasswordValid)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any(p => p == "Admin"))
                {
                    return Redirect(model.ReturnUrl ?? "/Admin");
                }
                return Redirect(model.ReturnUrl ?? "/");
            }

            
            

        }
        else
        {
            ModelState.AddModelError("", "UserName or Password is invalid.");
        }
        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            await _signInManager.SignOutAsync();
        }
        return Redirect("/");
    }
    [AllowAnonymous]
    public IActionResult Register()
    {
        RegisterViewModel model= new RegisterViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.Mobile,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var role = await _roleManager.FindByIdAsync("2");
                var result2 = await _userManager.AddToRoleAsync(user, role.Name);
                if (result2.Succeeded)
                    return RedirectToAction("Index","Home");
                foreach (var item in result2.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            
        }
        return View(model);
    }
}
