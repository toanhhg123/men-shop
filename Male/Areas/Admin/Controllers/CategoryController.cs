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
    public class CategoryController : Controller
    {
        private readonly MyDBContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public CategoryController(MyDBContext dBContext, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _DbContext = dBContext;
        }

        public IActionResult Index()
        {
            return View(_DbContext.Categories);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind] Category category)
        {
            if (!ModelState.IsValid) return View(category);
            await _DbContext.Categories.AddAsync(category);
            await _DbContext.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage("add Category success !");
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid) return RedirectToAction("index");
            var category = await _DbContext.Categories.FirstOrDefaultAsync(x => x.id == id);
            if (category != null)
            {
                _DbContext.Categories.Remove(category);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("remove success !");

            }
            return RedirectToAction("index");
        }

    }
}