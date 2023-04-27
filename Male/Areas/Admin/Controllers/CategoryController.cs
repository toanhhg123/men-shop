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

        public IActionResult Edit(string id)
        {
            var category = _DbContext.Categories.FirstOrDefault(x => x.id == id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind] Category category)
        {
            try
            {
                if (!ModelState.IsValid) return View(category);
                await _DbContext.Categories.AddAsync(category);
                await _DbContext.SaveChangesAsync();

                _toastNotification.AddSuccessToastMessage("add Category success !");
                return RedirectToAction("index");
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
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

        [HttpPost]
        public IActionResult Edit([Bind] Category category)
        {
            try
            {
                var categoryDb = _DbContext.Categories.FirstOrDefault(x => x.id == category.id);
                if (categoryDb != null)
                {
                    _DbContext.Entry(categoryDb).CurrentValues.SetValues(category);
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