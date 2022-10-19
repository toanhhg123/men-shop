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

        public async Task<IActionResult> Index(int? pageIndex, string? search)
        {
            var products = from s in _dbContext.Products select s;
            if(search != null)
               products = from s in _dbContext.Products where s.Name.Contains(search) select s;            

            // pagination            
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
                var productRelated = _dbContext.Products.Take(5) ;
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