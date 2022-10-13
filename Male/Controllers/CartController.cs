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
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly MyDBContext _dbContext;

        private readonly IUserService _userService;
        private readonly IToastNotification _toast;

        public CartController(ILogger<CartController> logger, MyDBContext dBContext, IUserService userService, IToastNotification toastNotification)
        {
            _logger = logger;
            _dbContext = dBContext;
            _userService = userService;
            _toast = toastNotification;
        }

        public IActionResult Index()
        {
            var userId = _userService.getUserId();
            var carts = _dbContext.Carts.Include(x => x.product).Include(x => x.Account).Where(x => x.Account.id.Equals(userId))
                .ToList();

            return View(carts);
        }

        public IActionResult GetCarts() => Ok(
            _dbContext.Carts.Include(x => x.product).Include(x => x.Account).Where(x => x.Account.id.Equals(_userService.getUserId()))
        );

        [HttpPost]
        public async Task<IActionResult> AddToCart(Cart cart, string productId, string? returnUrl)
        {
            returnUrl = returnUrl ?? "/cart";
            try
            {

                var findCard = _dbContext.Carts.SingleOrDefault(
                    x => x.product.id.Equals(productId) && x.Account.id.Equals(_userService.getUserId())
                );
                if (findCard != null)
                {
                    _toast.AddInfoToastMessage("product is exits in cart");
                    return Redirect(returnUrl);
                }
                cart.Account = _dbContext.Accounts.First(x => x.id == _userService.getUserId());
                cart.product = _dbContext.Products.First(x => x.id == productId);
                await _dbContext.Carts.AddAsync(cart);

                await _dbContext.SaveChangesAsync();
                _toast.AddSuccessToastMessage("product is added to cart");
                return Redirect(returnUrl);

            }
            catch
            {
                return RedirectToAction("Forbidden", "auth");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var returnUrl = "/cart";
            try
            {

                var findCard = _dbContext.Carts.SingleOrDefault(
                   x => x.id.Equals(id)
                );
                if (findCard == null)
                {
                    _toast.AddInfoToastMessage("not found product in cart");
                    return Redirect(returnUrl);
                }
                _dbContext.Carts.Remove(findCard);
                await _dbContext.SaveChangesAsync();
                _toast.AddSuccessToastMessage("remove product to card success");
                return Redirect(returnUrl);

            }
            catch
            {
                return RedirectToAction("Forbidden", "auth");
            }
        }



    }
}