using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            ViewBag.Categories = _dbContext.Categories;
            return View(_dbContext.Products.ToList());
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