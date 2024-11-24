using CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class AdminController : Controller
    {
        DotNetCrudContext db = new DotNetCrudContext();
        public IActionResult Index()
        {
            return View();
        }
        ////---- Products -----////

        [HttpGet]
        public IActionResult AddPro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPro(Product pg, IFormFile file)
        {
            var imageName = Path.GetFileName(file.FileName);
            string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Pro_img/");
            string imagevalue = Path.Combine(imagePath, imageName);
            using (var stream = new FileStream(imagevalue, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var dbimage = Path.Combine("/Image/Pro_img/", imageName);
            pg.Image = dbimage;
            db.Products.Add(pg);
            db.SaveChanges();

            return View();
        }
        public IActionResult ShowPro()
        {
            var productData = db.Products.ToList();
            return View(productData);
        }


        [HttpGet]
        public IActionResult DeletePro(Product pg)
        {

            db.Products.Remove(pg);
            db.SaveChanges();
            return RedirectToAction("ShowPro");

        }


        [HttpGet]
        public IActionResult EditPro(int Id)
        {
            var product = db.Products.Find(Id);
            return View(product);
        }
        [HttpPost]
        public IActionResult EditPro(Product pg, IFormFile file, string productid)
        {
            var dbimg = "";
            if (file != null && file.Length > 0)
            {
                var imageName = Path.GetFileName(file.FileName);
                string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Pro_img/");
                string imagevalue = Path.Combine(imagePath, imageName);
                using (var stream = new FileStream(imagevalue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                dbimg = Path.Combine("/Image/Pro_img/", imageName);
                pg.Image = dbimg;
                db.Products.Update(pg);
                db.SaveChanges();
            }
            else
            {
                pg.Image = productid;
                db.Products.Update(pg);
                db.SaveChanges();

            }
            return RedirectToAction("ShowPro");

        }
    }
}
