using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Male.Config;
using Male.Models;
using Male.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NToastNotify;

namespace Male.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class ProductController : Controller
    {
        private readonly MyDBContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public ProductController(MyDBContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View(_DbContext.Products.Include(x => x.Brand).Include(x => x.category).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.brands = _DbContext.Brands.ToList();
            ViewBag.Categories = _DbContext.Categories.ToList();
            return View();
        }

        public IActionResult Edit(string id)
        {
            ViewBag.brands = _DbContext.Brands.ToList();
            ViewBag.Categories = _DbContext.Categories.ToList();
            var product = _DbContext.Products.FirstOrDefault(x => x.id == id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind] ProductUpload product, string brandId, string categoryId)
        {
            ViewBag.brands = _DbContext.Brands.ToList();
            ViewBag.Categories = _DbContext.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            List<string> files = HandleFile.UploadMultipleFile(product.files);
            var brand = await _DbContext.Brands.FirstOrDefaultAsync(x => x.id == brandId);
            var category = await _DbContext.Categories.FirstOrDefaultAsync(x => x.id == categoryId);

            while (files.Count < 5)
                files.Add("");

            Product product1 = new Product()
            {
                Name = product.Name,
                description = product.description,
                Price = product.Price,
                CountStock = product.CountStock,
                img1 = files[0],
                img2 = files[1],
                img3 = files[2],
                img4 = files[3],
                Brand = brand,
                category = category
            };

            _DbContext.Products.Add(product1);
            await _DbContext.SaveChangesAsync();

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Edit([Bind] Product product, string category, string brand)
        {

            try
            {
                var productDb = _DbContext.Products.FirstOrDefault(x => x.id == product.id);
                if (productDb == null) throw new Exception("not found product");
                _DbContext.Entry(productDb).CurrentValues.SetValues(product);
                product.category = _DbContext.Categories.FirstOrDefault(x => x.id == category) ?? product.category;
                product.Brand = _DbContext.Brands.FirstOrDefault(x => x.id == brand) ?? product.Brand;
                _DbContext.SaveChanges();
                _toastNotification.AddSuccessToastMessage("update success");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            try
            {
                var productDb = await _DbContext.Products.FirstOrDefaultAsync(x => x.id == id);
                if (productDb == null) throw new Exception("not found product");

                _DbContext.Products.Remove(productDb);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("update success");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}