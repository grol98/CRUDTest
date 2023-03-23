using CRUDTest.Models;
using CRUDTest.Repository;

namespace CRUDTest.Interfaces
{
    public interface IOrderItemWebRep
    {
        public List<OrderItemWeb> GetOrderItemsWeb(int orderId);
        public List<string> GetAllItemName();
        public List<string> GetAllItemUnit();
        public void Add(int orderId, OrderItemWeb orderItemWeb);
        public void UpdateAll(int orderId, List<OrderItemWeb> orderItemWeb);
        public void RemoveAllByOrderId(int orderId);
    }
}
