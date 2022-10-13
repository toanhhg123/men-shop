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


namespace Male.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class BrandController : Controller
    {
        private readonly MyDBContext _DbContext;


        private readonly ConfigRoles _configRoles;

        public BrandController(MyDBContext dBContext, IOptions<ConfigRoles> configRoles)
        {
            _configRoles = configRoles.Value;
            _DbContext = dBContext;
        }

        public IActionResult Index()
        {
            return View(_DbContext.Brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);
            await _DbContext.Brands.AddAsync(brand);
            await _DbContext.SaveChangesAsync();

            return RedirectToAction("index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid) return RedirectToAction("index");
            var brand = await _DbContext.Brands.FirstOrDefaultAsync(x => x.id == id);
            if (brand != null)
            {
                _DbContext.Brands.Remove(brand);
                await _DbContext.SaveChangesAsync();
            }
            return RedirectToAction("index");
        }




    }
}