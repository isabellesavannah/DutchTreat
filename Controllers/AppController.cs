using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IDutchRepository _repository;
        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
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
        [Authorize]
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();
            
            return View(results);
        }
    }
}
