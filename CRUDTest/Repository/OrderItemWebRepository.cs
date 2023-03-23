using CRUDTest.Interfaces;
using CRUDTest.Models;
using System.Collections.Generic;

namespace CRUDTest.Repository
{
    public class OrderItemWebRepository : IOrderItemWebRep
    {
        private ApplicationContext dbcontext;
        public OrderItemWebRepository(ApplicationContext context) 
        {
            dbcontext = context;
        }

        public List<OrderItemWeb> GetOrderItemsWeb(int orderId)
        {
            var orderItems = dbcontext.OrderItem.Where(p => p.OrderId == orderId).ToList();
            List<OrderItemWeb> orderItemsWeb = new List<OrderItemWeb>();
            foreach (var item in orderItems)
            {
                OrderItemWeb orderItemWeb = new OrderItemWeb();
                orderItemWeb.Name = item.Name;
                orderItemWeb.Quantity = item.Quantity;
                orderItemWeb.Unit = item.Unit;
                orderItemsWeb.Add(orderItemWeb);
            }
            return orderItemsWeb;
        }

        public List<string> GetAllItemName()
        {
            return dbcontext.OrderItem.Select(p => p.Name).Distinct().ToList();
        }

        public List<string> GetAllItemUnit()
        {
            return dbcontext.OrderItem.Select(p => p.Unit).Distinct().ToList();
        }

        public void Add(int orderId, OrderItemWeb orderItemWeb)
        {
            OrderItemDB orderItem = new OrderItemDB();
            orderItem.OrderId = orderId;
            orderItem.Name = orderItemWeb.Name;
            orderItem.Quantity = orderItemWeb.Quantity;
            orderItem.Unit = orderItemWeb.Unit;
            dbcontext.OrderItem.Add(orderItem);
            dbcontext.SaveChanges();
        }

        public void UpdateAll(int orderId, List<OrderItemWeb> orderItemWeb)
        {
            RemoveAllByOrderId(orderId);
            foreach (OrderItemWeb item in orderItemWeb)
            {
                Add(orderId, item);
            }
        }

        public void RemoveAllByOrderId(int orderId)
        {
            dbcontext.OrderItem.RemoveRange(dbcontext.OrderItem.Where(p => p.OrderId == orderId).ToList());
            dbcontext.SaveChanges();
        }
    }
}