using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Male.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Male.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly MyDBContext _dbContext;

        public ShopController(ILogger<ShopController> logger, MyDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        public async Task<IActionResult> Index(int? pageIndex, string? search, int? fromPrice, int? toPrice, string? brand, string? category, string? sort = "ASC")
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.search = search;
            ViewBag.category = category;
            ViewBag.brand = brand;
            ViewBag.sort = sort;
            ViewBag.fromPrice = fromPrice;
            ViewBag.toPrice = toPrice;



            // Handle links
            var products = from s in
                        _dbContext.Products.Include(x => x.category).Include(x => x.Brand)
                           select s;
            if (brand != null)
                products = from s in products
                           where s.Brand != null && s.Brand.Name.Equals(brand)
                           select s;
            if (category != null)
                products = from s in products
                           where s.category != null && s.category.Name.Equals(category)
                           select s;
            if (search != null)
                products = from s in products
                           where s.Name.Contains(search)
                           select s;
            if (sort != null)
                products = sort == "DESC" ?
                         (from s in products
                          orderby s.Price descending
                          select s) :
                        (from s in products
                         orderby s.Price ascending
                         select s);
            if (fromPrice != null && toPrice != null)
                products = from s in products
                           where s.Price >= fromPrice && s.Price <= toPrice
                           select s;
            // pagiation
            int pageSize = 8;
            int pageNumber = pageIndex ?? 1;
            var productsPg = await PaginatedList<Product>.CreateAsync(products, pageNumber, pageSize);
            ViewBag.Categories = _dbContext.Categories;
            ViewBag.productsPg = productsPg;


            return View(productsPg);
        }

        public IActionResult Detail(string id)
        {
            try
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.id == id);
                if (product == null) throw new Exception("Not found product");
                var productRelated = _dbContext.Products.Take(5);
                ViewBag.productRelated = productRelated;
                return View(product);
            }
            catch
            {
                return Redirect("/auth/forbidden/");
            }

        }




    }
}