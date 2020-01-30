/* Interface for Order Repository */

namespace JoysCakeShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
