using CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class UserController : Controller
    {
        DotNetCrudContext db = new DotNetCrudContext();

        public IActionResult Index()
        {
            var productData = db.Products.ToList();
            return View(productData);
        }
        [HttpPost]
        public IActionResult Search(string xyz)
        {
            // If the search term is not empty, filter the products
            var resultt = string.IsNullOrEmpty(xyz)
                ? db.Products.ToList() // If no search term, return all products
                : db.Products.Where(x => x.Name.Contains(xyz)).ToList();

            // Return the filtered results to the view
            return View("Index", resultt);
        }

    }
}
