using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoysCakeShop.Models;
using JoysCakeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JoysCakeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICakeRepository _cakeRespository;

        public HomeController(ICakeRepository CakeRepository)
        {
            _cakeRespository = CakeRepository;
        }
        public IActionResult Index()
        {
            HomeViewModel homeviewmodel = new HomeViewModel();
            homeviewmodel.CakesoftheWeek = _cakeRespository.CakesoftheWeek;   
            
            return View(homeviewmodel);
        }
    }
}
