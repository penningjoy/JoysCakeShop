using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace JoysCakeShop.Models
{
    public class CakeRepository:ICakeRepository
    {
        private readonly CakesDbContext _cakesDbContext;
        public CakeRepository(CakesDbContext cakesdbcontext)
        {
            _cakesDbContext = cakesdbcontext;
        }

        public IEnumerable<Cake> AllCakes
        {
            get
            {
                return _cakesDbContext.Cakes.Include(c => c.Category);
            }
        }
        public IEnumerable<Cake> CakesoftheWeek
        {
            get
            {
                return _cakesDbContext.Cakes.Include(c => c.Category).Where(ck => ck.IsCakeoftheWeek == true);
            }
        }

        public Cake getCakebyId(int CakeId)
        {
           return _cakesDbContext.Cakes.FirstOrDefault(ck => ck.CakeId == CakeId);
        }
    }
}
