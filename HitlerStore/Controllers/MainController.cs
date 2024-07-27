using HitlerStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HitlerStore.Controllers
{
    public class MainController : Controller
    {
        private readonly ProductDbContext context;

        public MainController(ProductDbContext context)
        {
            this.context = context;
        }

        public IActionResult HomePage(string sort, string search)
        {
            var products = from p in context.ProductsTable select p;


            ViewData["sort"] = String.IsNullOrEmpty(sort) ? "sort_pr" : "";
            ViewData["search"] = search;

            switch (sort)
            {
                case "sort_pr":
                    products = products.OrderBy(p => p.ProductName);
                    break;

                default:
                    products = products.OrderBy(p => p.Id);
                    break;
            }


            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ProductName.Contains(search) || p.ProductBio.Contains(search) || p.ProductCategory.Contains(search));
            }

            return View(products.AsNoTracking().ToList());
        }

        public IActionResult Add()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Add(Product addProduct)
        {
            if (ModelState.IsValid)
            {
                if (addProduct.ProductImageName != null && addProduct.ProductImageName.Length > 0)
                {
                    // Yükləmə qovluğunun yolu
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hitler");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var fileName = Path.GetFileName(addProduct.ProductImageName);
                    var filePath = Path.Combine(uploadDir, fileName);

                    // Məhsul məlumatlarına şəkil yolunu əlavə et
                    addProduct.ProductImageName="/wwwroot/" + fileName;
                }

                // Məhsulu veritabanına əlavə et
                 context.ProductsTable.Add(addProduct);
                 context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            var list = context.ProductsTable.ToList();

            return View("Home", list);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var find = context.ProductsTable.Find(id);
            
            if(find==null)
            {
                return NotFound();
            }

            if (!String.IsNullOrEmpty(find.ProductImageName))
            {
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",find.ProductImageName.TrimStart('/'));
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
            }

            context.ProductsTable.Remove(find);
            context.SaveChanges();

            var listAfterDelete = context.ProductsTable.ToList();
            return View("HomePage", listAfterDelete);
            // return RedirectToAction("HomePage",listAfterDelete); 
        }


        public IActionResult  Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var find =  context.ProductsTable.Find(id);
            if (find == null)
            {
                return NotFound();
            }

            ViewData["spId"] = find.SpeacialId;
            ViewData["name"] = find.ProductName;
            ViewData["price"] = find.ProductPrice;
            ViewData["bio"] = find.ProductBio;
            ViewData["image"] = find.ProductImageName;
            ViewData["category"] = find.ProductCategory;
            return View();
        }

    }
}
