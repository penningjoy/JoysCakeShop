using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoysCakeShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private CakesDbContext _cakesDbContext;
        public CategoryRepository(CakesDbContext cakesDbContext)
        {
            _cakesDbContext = cakesDbContext;
        }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _cakesDbContext.Categories;
            }
        }

    }
}
