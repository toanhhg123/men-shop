using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Male.Config;
using Male.Models;
using Male.utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NToastNotify;

namespace Male.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly MyDBContext _dbCOntext;

        private readonly IToastNotification _toast;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ConfigRoles _configRoles;

        public AuthController(ILogger<AuthController> logger, MyDBContext dBContext, IHttpContextAccessor httpContext, IToastNotification toastNotification, IOptions<ConfigRoles> configRoles)
        {
            _logger = logger;
            _configRoles = configRoles.Value;
            _dbCOntext = dBContext;
            _httpContext = httpContext;
            _toast = toastNotification;
        }

        public IActionResult Login(string? returnUrl)
        {
            returnUrl = returnUrl ?? "/";
            ViewBag.returnUrl = returnUrl;
            if (User?.Identity?.IsAuthenticated == true)
                return Redirect(returnUrl);
            return View();
        }

        [HttpGet]
        public IActionResult SignUp(string? returnUrl)
        {
            returnUrl = returnUrl ?? "/";
            ViewBag.returnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegister userRegister)
        {

            try
            {
                if (!ModelState.IsValid) return View(userRegister);
                if (userRegister.password != userRegister.confirmPassword) throw new Exception("Pasword and confirm not match");

                var roleCustomer = _dbCOntext.Roles.First(x => x.RoleName == _configRoles.RoleCustomer);
                var findUser = _dbCOntext.Accounts.FirstOrDefault(x => x.email == userRegister.email);

                if (findUser != null) throw new Exception("User is eraly exits");

                Account newA = new Account()
                {
                    userName = userRegister.userName,
                    salt = Guid.NewGuid().ToString(),
                    email = userRegister.email,
                    Role = roleCustomer

                };



                newA.hashPassword = MD5Password.HashPass(userRegister.password, newA.salt);


                _dbCOntext.Accounts.Add(newA);

                await _dbCOntext.SaveChangesAsync();

                _toast.AddSuccessToastMessage("sign Success, Login now, Please!!!");
                return Redirect(nameof(Login));

            }
            catch (System.Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string returnUrl)
        {
            returnUrl = returnUrl ?? "/";
            ViewBag.returnUrl = returnUrl;


            try
            {
                Account user = validateUser(email, password);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.email),
                    new Claim("username", user.userName),
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Logout(string? returnUrl)
        {
            returnUrl = returnUrl ?? "/auth/login";

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Forbidden()
        {

            await HttpContext.SignOutAsync(
               CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        private Account validateUser(string email, string password)
        {
            try
            {
                var user = _dbCOntext.Accounts.Include(x => x.Role).FirstOrDefault(x => x.email == email);
                if (user == null)
                    throw new Exception("email is incorrect ");
                if (!MD5Password.ComparePassword(password, user.salt, user.hashPassword))
                    throw new Exception("password is incorrect");
                return user;
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}