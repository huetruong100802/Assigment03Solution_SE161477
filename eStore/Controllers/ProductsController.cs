using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public ProductsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

       
    }
}
