using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Male.Controllers
{
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly MyDBContext _dbContext;

        public BlogController(ILogger<BlogController> logger, MyDBContext context)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _dbContext.Blogs.ToListAsync();
            return View(blogs);
        }

        
        [Route("{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.id == id);
            if(blog != null)
                return View(blog);
            return BadRequest(id);
        }


    }
}