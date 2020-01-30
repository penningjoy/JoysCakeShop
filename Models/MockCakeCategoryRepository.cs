using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoysCakeShop.Models
{
    public class MockCakeCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>  new List<Category> {
            new Category{ CategoryId = 1, CategoryName = "Fruit Cake", Description = " Best Fruit Cake "  },
            new Category { CategoryId = 2, CategoryName = "Cheese Cake", Description = " Best Cheese Cake " }
        }; 
    }
}
