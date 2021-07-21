using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly DutchContext _context;

        public AppController(IMailService mailService, DutchContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //----------------------------------------------------- Get Contact
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        //----------------------------------------------------- Add Contact/Message
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {   
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("isabellet.208@gmail.com", model.Subject, $"From:{model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            return View();

        }
        //----------------------------------------------------- Return About Us
        public IActionResult About()
        {
            ViewBag.Title = "About us";
            return View();
        }

        //----------------------------------------------------- Shopppp/Return Products
        public IActionResult Shop()
        {
            var results = from p in _context.Products
                          orderby p.Category
                          select p;
            return View(results.ToList());
        }
    }
}
