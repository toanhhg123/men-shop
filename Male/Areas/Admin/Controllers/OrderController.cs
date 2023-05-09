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
    [Authorize(Roles = "ADMIN")]
    public class OrderController : Controller
    {
        private readonly MyDBContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public OrderController(MyDBContext dBContext, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _DbContext = dBContext;
        }

        public IActionResult Index()
        {
            return View(_DbContext.Orders.Include(x => x.product).Include(x => x.account).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(string id)
        {
            var order = await _DbContext.Orders.FirstAsync(x => x.id.Equals(id));

            order.isConfirm = true;

            await _DbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UnConfirm(string id)
        {
            var order = await _DbContext.Orders.FirstAsync(x => x.id.Equals(id));

            order.isConfirm = false;

            await _DbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var order = await _DbContext.Orders.FirstOrDefaultAsync(x => x.id.Equals(id));
                if (order == null) throw new Exception("not found order");
                _DbContext.Orders.Remove(order);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("success");
                return Redirect("/admin/order");

            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return Redirect("/admin/order");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var order = await _DbContext.Orders.FirstOrDefaultAsync(x => x.id.Equals(id));
                return View(order);

            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return Redirect("/admin/order");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order orderCur)
        {
            try
            {
                var order = await _DbContext.Orders.FirstOrDefaultAsync(x => x.id.Equals(orderCur.id));
                if (order == null) throw new Exception("not found order");
                order.status = orderCur.status;

                await _DbContext.SaveChangesAsync();
                return View(order);
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return Redirect("/admin/order");
            }

        }

    }
}