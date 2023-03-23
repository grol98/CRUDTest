using CRUDTest.Interfaces;
using CRUDTest.Models;

namespace CRUDTest.Repository
{
    public class OrderRepository : IOrderRep
    {
        private ApplicationContext dbcontext;
        private IOrderItemWebRep orderItemWebRep;
        public OrderRepository(ApplicationContext context, IOrderItemWebRep orderItemWebRep) 
        {
            dbcontext = context;
            this.orderItemWebRep = orderItemWebRep;
        }

        public OrderDB? GetOrderById(int id) => dbcontext.Order.FirstOrDefault(p => p.Id == id);

        public OrderDB? GetOrderByNumberAndProvider(string orderNumber, int ProviderId) => dbcontext.Order.FirstOrDefault(p => p.Number == orderNumber && p.ProviderId == ProviderId);
        

        IEnumerable<OrderDB> IOrderRep.GetOrders => dbcontext.Order.ToList();

        public void AddOrder(OrderWeb orderWeb)
        {
            OrderDB order = new OrderDB();
            order.Number = orderWeb.Number;
            order.Date = orderWeb.Date.AddHours(3).ToUniversalTime();
            order.ProviderId = orderWeb.ProviderId;
            dbcontext.Order.Add(order);
            dbcontext.SaveChanges();
        }

        public void UpdateOrder(int id, OrderWeb orderWeb)
        {
            OrderDB order = GetOrderById(id);
            order.Number = orderWeb.Number;
            order.Date = orderWeb.Date.AddHours(3).ToUniversalTime();
            order.ProviderId = orderWeb.ProviderId;
            dbcontext.Order.Update(order);
            dbcontext.SaveChanges();
        }

        public void Remove(string orderNumber, int providerId)
        {
            var order = GetOrderByNumberAndProvider(orderNumber, providerId);
            orderItemWebRep.RemoveAllByOrderId(order.Id);
            dbcontext.Remove(order);
            dbcontext.SaveChanges();
        }
    }
}