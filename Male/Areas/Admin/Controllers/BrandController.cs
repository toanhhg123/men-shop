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
    public class BrandController : Controller
    {
        private readonly MyDBContext _DbContext;
        private readonly IToastNotification _toastNotification;




        private readonly ConfigRoles _configRoles;

        public BrandController(MyDBContext dBContext, IOptions<ConfigRoles> configRoles, IToastNotification toastNotification)
        {
            _configRoles = configRoles.Value;
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View(_DbContext.Brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(string id)
        {
            var brand = _DbContext.Brands.FirstOrDefault(x => x.id == id);
            return View(brand);
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

        [HttpPost]
        public IActionResult Edit([Bind] Brand brand)
        {
            try
            {
                var brandDb = _DbContext.Brands.FirstOrDefault(x => x.id == brand.id);
                if (brandDb != null)
                {
                    _DbContext.Entry(brandDb).CurrentValues.SetValues(brand);
                    _DbContext.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("update success");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }


    }
}