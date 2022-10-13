using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Male.Config;
using Male.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Male.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class HomeController : Controller
    {
        private readonly MyDBContext _DbContext;


        private readonly ConfigRoles _configRoles;

        public HomeController(MyDBContext dBContext, IOptions<ConfigRoles> configRoles)
        {
            _configRoles = configRoles.Value;
            _DbContext = dBContext;
        }

        public IActionResult Index()
        {
            return View();
        }

      
    }
}