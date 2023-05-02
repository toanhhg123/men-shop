using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Male.Controllers
{
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly MyDBContext _dbCOntext;
        public ContactController(ILogger<ContactController> logger, MyDBContext dBContext)
        {
            _dbCOntext = dBContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var contacts = _dbCOntext.Contacts.ToList();
            return View(contacts);
        }


    }
}