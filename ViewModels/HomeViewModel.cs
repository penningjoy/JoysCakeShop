using JoysCakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoysCakeShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Cake> CakesoftheWeek { get; set; }
    }
}
