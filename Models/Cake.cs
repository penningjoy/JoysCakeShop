/* --------------Cake Model Class -----------------------*/
/* The Database Table Cake is built using this same model */

namespace JoysCakeShop.Models
{
    public class Cake
    {
        public int CakeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool InStock { get; set; }
        public bool IsCakeoftheWeek { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
