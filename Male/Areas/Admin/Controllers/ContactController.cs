using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Male.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Male.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class ContactController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IToastNotification _toastNotification;
        public ContactController(MyDBContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _dbContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var contacts = await _dbContext.Contacts.ToListAsync();
            return View(contacts);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            try
            {
                await _dbContext.Contacts.AddAsync(contact);
                await _dbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("success");

                return Redirect("/admin/contact/Index");
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.id == id);
            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                await _dbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("success");

            }
            return Redirect("/admin/contact/Index");

        }

        public async Task<IActionResult> Edit(string id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.id == id);
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contactNew)
        {
            try
            {
                var contactDb = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.id == contactNew.id);
                if (contactDb == null) throw new Exception("not found contact");
                _dbContext.Entry(contactDb).CurrentValues.SetValues(contactNew);
                await _dbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("success");
                return View(contactNew);
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);    // TODO
            }
        }
    }
}