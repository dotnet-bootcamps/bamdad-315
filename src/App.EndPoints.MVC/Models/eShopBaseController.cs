using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.EndPoints.MVC.Models
{
    public class eShopBaseController : Controller
    {

        protected string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
