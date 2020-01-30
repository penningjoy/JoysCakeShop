using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoysCakeShop.Models;
using JoysCakeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JoysCakeShop.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _CakeRepository;
        private readonly ICategoryRepository _CategoryRepository;

        public CakeController(ICakeRepository cakerepo, ICategoryRepository categoryrepo)
        {
            _CakeRepository = cakerepo;
            _CategoryRepository = categoryrepo;
        }

        public ViewResult List()
        {
            CakesListViewModel cakeslistviewmodel = new CakesListViewModel();
            cakeslistviewmodel.Cakes = _CakeRepository.AllCakes;
            cakeslistviewmodel.CurrentCategory = "Our Finest Collection of Cakes curated to your needs!";
            return View(cakeslistviewmodel);
        }

        public IActionResult Details(int id)
        {
            CakesDetailsViewModel cakesdetailsviewmodel = new CakesDetailsViewModel();
            var cake = _CakeRepository.getCakebyId(id);

            cakesdetailsviewmodel.CakeId = cake.CakeId;
            cakesdetailsviewmodel.Name = cake.Name;
            cakesdetailsviewmodel.Description = cake.Description;
            cakesdetailsviewmodel.Price = cake.Price;
            cakesdetailsviewmodel.ImageUrl = cake.ImageUrl;
            
            if (cakesdetailsviewmodel == null) 
                return NotFound();
            else
            {
                return View(cakesdetailsviewmodel);
            }


        }
    }
}
