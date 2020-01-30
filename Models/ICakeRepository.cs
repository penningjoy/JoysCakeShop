using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoysCakeShop.Models
{
    public interface ICakeRepository
    {
        IEnumerable<Cake> AllCakes { get; }
        IEnumerable<Cake> CakesoftheWeek { get; }
        Cake getCakebyId(int CakeId);
    }
}
