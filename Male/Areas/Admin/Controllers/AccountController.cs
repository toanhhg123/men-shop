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


namespace Male.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class AccountController : Controller
    {
        private readonly MyDBContext _DbContext;


        private readonly ConfigRoles _configRoles;

        public AccountController(MyDBContext dBContext, IOptions<ConfigRoles> configRoles)
        {
            _configRoles = configRoles.Value;
            _DbContext = dBContext;
        }

        public async Task<IActionResult> Index()
        {

            var accounts = await _DbContext.Accounts.Include(a => a.Role).ToListAsync();
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.roles = _DbContext.Roles.ToList();
            return View();
        }


        // [HttpGet]
        // public async Task<IActionResult> SeedRoles()
        // {
        //     List<Role> roles = new List<Role>() {
        //         new Role() {RoleName = _configRoles.RoleAdmin},
        //         new Role() {RoleName = _configRoles.RoleHr},
        //         new Role() {RoleName = _configRoles.RoleCustomer},

        //     };
        //     await _DbContext.Roles.AddRangeAsync(roles);
        //     await _DbContext.SaveChangesAsync();

        //     return Ok(roles);
        // }

        [HttpPost]
        public async Task<IActionResult> Create([Bind] UserRegister user, IFormFile? img, string? province, string? districts, string? role)
        {
            ViewBag.roles = _DbContext.Roles.ToList();

            if (!ModelState.IsValid)
            {
                return View(user);
            }


            user.address = province + districts;
            var roleSelect = _DbContext.Roles.FirstOrDefault(r => r.id == role);
            Account account = new Account()
            {
                userName = user.userName,
                email = user.email,
                address = province != null && districts != null ? province + districts : "",
                salt = Guid.NewGuid().ToString(),
                phoneNumber = user.phoneNumber,
                img = HandleFile.UploadSingleFile(img),
                Role = roleSelect ?? new Role()
            };
            account.hashPassword = MD5Password.HashPass(user.password, account.salt);

            await _DbContext.Accounts.AddAsync(account);
            await _DbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}