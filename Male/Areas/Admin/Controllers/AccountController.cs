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
    // [Authorize(Roles = "ADMIN")]
    public class AccountController : Controller
    {
        private readonly MyDBContext _DbContext;


        private readonly ConfigRoles _configRoles;
        private readonly IToastNotification _toastNotification;


        public AccountController(MyDBContext dBContext, IOptions<ConfigRoles> configRoles, IToastNotification toastNotification)
        {
            _configRoles = configRoles.Value;
            _DbContext = dBContext;
            _toastNotification = toastNotification;
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

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.roles = _DbContext.Roles.ToList();

            try
            {
                var account = await _DbContext.Accounts.Include(x => x.Role).FirstOrDefaultAsync(x => x.id == id);
                if (account == null) throw new Exception("not found account");
                return View(account);
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Account accountNew, string role)
        {
            ViewBag.roles = _DbContext.Roles.ToList();
            try
            {
                var account = await _DbContext.Accounts.FirstOrDefaultAsync(x => x.id == accountNew.id);

                if (account == null) throw new Exception("not found account");
                _DbContext.Entry(account).CurrentValues.SetValues(accountNew);
                account.Role = await _DbContext.Roles.FirstOrDefaultAsync(x => x.id == role) ?? accountNew.Role;
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("update success");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Detete(string id)
        {
            ViewBag.roles = _DbContext.Roles.ToList();
            try
            {
                var account = await _DbContext.Accounts.FirstOrDefaultAsync(x => x.id == id);

                if (account == null) throw new Exception("not found account");
                _DbContext.Accounts.Remove(account);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("delete success");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }




    }
}