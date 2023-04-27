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
    public class BlogController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IToastNotification _toastNotification;
        public BlogController(MyDBContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _dbContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var blogs = await _dbContext.Blogs.ToListAsync();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(string id)
        {
            var blog = _dbContext.Blogs.FirstOrDefault(x => x.id == id);
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog, IFormFile img)
        {

            blog.Img = HandleFile.UploadSingleFile(img);
            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("success");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.id == id);
            if (blog != null)
            {
                _dbContext.Blogs.Remove(blog);
                await _dbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("success");

            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Edit([Bind] Blog blog)
        {
            try
            {
                var blogDb = _dbContext.Blogs.FirstOrDefault(x => x.id == blog.id);
                if (blogDb == null) throw new Exception("not fond blog");
                _dbContext.Entry(blogDb).CurrentValues.SetValues(blog);
                _dbContext.SaveChanges();
                _toastNotification.AddSuccessToastMessage("success");
                return Redirect("/admin/blog");
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}