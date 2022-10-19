using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Male.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        public async Task<IActionResult> Index()
        {
            var userId = _userService.getUserId();
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.id.Equals(userId));
            var userProfile = JsonConvert.DeserializeObject<UserProfile>(JsonConvert.SerializeObject(user));
            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserProfile userProfile, string province, string districts, IFormFile? img)
        {
            try
            {
                var userId = _userService.getUserId();
                var user = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.id.Equals(userId));
                if (user == null)
                    throw new Exception("user not found");
                if (province != "" && province != null)
                    user.address = province + " " + districts;
                if (img != null)
                    {
                        HandleFile.DeleteFile(user.img ?? "");
                        user.img = HandleFile.UploadSingleFile(img);
                    }
                user.phoneNumber = userProfile.phoneNumber ?? user.phoneNumber;

                await _dbContext.SaveChangesAsync();
                
                return View(nameof(Index));


            }
            catch (System.Exception e)
            {
                ViewBag.errorMessage = e.Message;
                return View(nameof(Index));
            }
        }





    }
}