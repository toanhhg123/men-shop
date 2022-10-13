using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;

namespace Male.Controllers
{
    [Authorize(Roles = "CUSTOMER")]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly MyDBContext _dbContext;

        private readonly IUserService _userService;
        private readonly IToastNotification _toast;

        public ProfileController(ILogger<ProfileController> logger,
        MyDBContext dBContext, IUserService userService,
        IToastNotification toastNotification)
        {
            _logger = logger;
            _dbContext = dBContext;
            _userService = userService;
            _toast = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }





    }
}