using CRUDTest.Models;

namespace CRUDTest.Interfaces
{
    public interface IOrderRep
    {
        public OrderDB? GetOrderById(int id);
        public OrderDB GetOrderByNumberAndProvider(string orderNumber, int ProviderId);
        public IEnumerable<OrderDB> GetOrders { get; }
        public void AddOrder(OrderWeb orderWeb);
        public void UpdateOrder(int id, OrderWeb orderWeb);
        public void Remove(string orderNumber, int providerId);
    }
}
