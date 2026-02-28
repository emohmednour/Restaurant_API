using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    public class DishesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
