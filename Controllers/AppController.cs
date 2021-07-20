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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {   
            if (ModelState.IsValid)
            {
                _mailService.SendMail("isabellet.208@gmail.com", model.Subject, $"From:{model.Email}, Message: {model.Message}");
            }
            return View();

        }

        public IActionResult About()
        {
            ViewBag.Title = "About us";
            return View();
        }
    }
}
