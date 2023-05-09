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
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly MyDBContext _dbContext;

        private readonly IUserService _userService;
        private readonly IToastNotification _toast;

        public OrderController(ILogger<OrderController> logger, MyDBContext dBContext, IUserService userService, IToastNotification toastNotification)
        {
            _logger = logger;
            _dbContext = dBContext;
            _userService = userService;
            _toast = toastNotification;
        }

        public IActionResult Index()
        {
            var userId = _userService.getUserId();
            var Orders = _dbContext.Orders.Include(x => x.product).Include(x => x.account).Where(x => x.account.id.Equals(userId))
                .ToList();
            return View(Orders);
        }




    }
}