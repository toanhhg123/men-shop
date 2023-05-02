using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Male.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Male.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class BannerController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IToastNotification _toastNotification;
        public BannerController(MyDBContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _dbContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var Banners = await _dbContext.Banners.ToListAsync();
            return View(Banners);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Banner banner, IFormFile img)
        {
            try
            {
                if (img == null) throw new Exception("you can upload file imgae");
                banner.src = HandleFile.UploadSingleFile(img);
                await _dbContext.Banners.AddAsync(banner);
                await _dbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("success");

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var banner = await _dbContext.Banners.FirstOrDefaultAsync(x => x.id == id);
            if (banner != null)
            {
                _dbContext.Banners.Remove(banner);
                await _dbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("success");

            }
            return RedirectToAction("Index");

        }

    }
}