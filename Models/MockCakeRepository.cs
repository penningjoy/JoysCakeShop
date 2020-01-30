using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoysCakeShop.Models
{
    public class MockCakeRepository : ICakeRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCakeCategoryRepository();

        public IEnumerable<Cake> AllCakes => new List<Cake>
        {
            new Cake{ 
                      CakeId = 1, Name = "Strawberry Cake", Description = " Best Strawberry Cake Available",Price = 5.40M,
                      ImageUrl= "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg" , 
                      InStock = true, IsCakeoftheWeek = false,
                      Category = _categoryRepository.AllCategories.ToList()[0]
                    },
            new Cake{
                      CakeId = 1, Name = "Pumpkin Cake",  Description = " Best Pumpkin Cake Available", Price = 8.40M,
                      ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg",
                      InStock = true, IsCakeoftheWeek = true,
                      Category = _categoryRepository.AllCategories.ToList()[0]
                    },
            new Cake{ 
                      CakeId = 1, Name = "Chocolate Forest Cake",  Description = " Best Apple Cake Available", Price = 27.30M,
                      ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg",
                      InStock = true, IsCakeoftheWeek = false,
                      Category = _categoryRepository.AllCategories.ToList()[1]
                    },
        };

        public IEnumerable<Cake> CakesoftheWeek { get;  }

        public Cake getCakebyId(int CakeId)
        {
            return AllCakes.FirstOrDefault(c => c.Price <= 10.00M);
        }

    }
}
