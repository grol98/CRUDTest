using CRUDTest.Interfaces;
using CRUDTest.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CRUDTest.Repository
{
    public class OrderWebRepository : IOrderWebRep
    {
        private IOrderRep orderRep;
        private IOrderItemWebRep orderItemWebRep;
        private IProviderRep providerRep;

        public OrderWebRepository(IOrderRep orderRep, IOrderItemWebRep orderItemWebRep, IProviderRep providerRep)
        {
            this.orderRep = orderRep;
            this.orderItemWebRep = orderItemWebRep;
            this.providerRep = providerRep;
        }

        public OrderWeb GetOrderWebById(int id)
        {
            var order = orderRep.GetOrderById(id);
            OrderWeb orderWeb = new OrderWeb();
            orderWeb.Number = order.Number;
            orderWeb.Date = order.Date;
            orderWeb.ProviderId = providerRep.GetProviderById(order.ProviderId).Id;
            orderWeb.OrderItemList = orderItemWebRep.GetOrderItemsWeb(id).ToList();
            return orderWeb;
        }

        public OrderWeb GetOrderWebByNumberAndProvider(string orderNumber, int ProviderId)
        {
            var order = orderRep.GetOrderByNumberAndProvider(orderNumber, ProviderId);
            OrderWeb orderWeb = new OrderWeb();
            orderWeb.Number = order.Number;
            orderWeb.Date = order.Date;
            orderWeb.ProviderId = providerRep.GetProviderById(order.ProviderId).Id;
            orderWeb.OrderItemList = orderItemWebRep.GetOrderItemsWeb(order.Id).ToList();
            return orderWeb;
        }

        public List<OrderWeb> GetOrdersWeb()
        {
            var orders = orderRep.GetOrders;
            List<OrderWeb> orderWebList = new List<OrderWeb>();
            foreach (var order in orders)
            {
                OrderWeb orderWeb = new OrderWeb();
                orderWeb.Number = order.Number;
                orderWeb.Date = order.Date;
                orderWeb.ProviderId = providerRep.GetProviderById(order.ProviderId).Id;
                orderWeb.OrderItemList = orderItemWebRep.GetOrderItemsWeb(order.Id).ToList();
                orderWebList.Add(orderWeb);
            }
            return orderWebList;
        }

        public List<OrderWeb> GetOrdersWebByFilter(string orderNumber, string itemName, string itemUnit, string timeBeg, string timeEnd, string providerName)
        {
            List<OrderWeb> ordersWeb = GetOrdersWeb();
            var orderNumbers = JsonSerializer.Deserialize<List<string>>(orderNumber);
            var itemNames = JsonSerializer.Deserialize<List<string>>(itemName);
            var itemUnits = JsonSerializer.Deserialize<List<string>>(itemUnit);
            var providersId = providerRep.GetProvidersIdByNames(JsonSerializer.Deserialize<List<string>>(providerName));
            if (orderNumbers.Count > 0)
            {
                ordersWeb = ordersWeb.Where(p => orderNumbers.Contains(p.Number)).ToList();
            }
            if (providersId.Count > 0)
            {
                ordersWeb = ordersWeb.Where(p => providersId.Contains(p.ProviderId)).ToList();
            }
            foreach (var itemNameLocal in itemNames)
            {
                ordersWeb = ordersWeb.Where(p => p.OrderItemList.Exists(d => itemNames.Contains(d.Name))).ToList();
            }
            foreach (var itemUnitLocal in itemUnits)
            {
                ordersWeb = ordersWeb.Where(p => p.OrderItemList.Exists(d => itemUnits.Contains(d.Unit))).ToList();
            }
            if(timeBeg is not null)
            {
                ordersWeb = ordersWeb.Where(p => p.Date > DateTime.ParseExact(timeBeg, "yyyy-MM-dd", null)).ToList();
            }
            if (timeEnd is not null)
            {
                ordersWeb = ordersWeb.Where(p => p.Date < DateTime.ParseExact(timeEnd, "yyyy-MM-dd", null)).ToList();
            }
            return ordersWeb;
        }

        public string AddOrder(OrderWeb orderWeb) // при успешном добавлении возвращает true
        {
            foreach(OrderItemWeb item in orderWeb.OrderItemList)
            {
                if(item.Name == orderWeb.Number)
                {
                    return "Имя продукта не должно совпадать с номером заказа";
                }
            }
            if (orderRep.GetOrderByNumberAndProvider(orderWeb.Number, orderWeb.ProviderId) != null)
            {
                return "Такой заказ от этого производителя уже есть";
            }
            orderRep.AddOrder(orderWeb);
            int orderId = orderRep.GetOrderByNumberAndProvider(orderWeb.Number, orderWeb.ProviderId).Id;
            foreach (OrderItemWeb item in orderWeb.OrderItemList)
            {
                orderItemWebRep.Add(orderId, item);
            }
            
            return "true";
        }

        public string EditOrder(OrderWeb orderWeb, string orderNumber, int providerId) // при успешном добавлении возвращает true
        {
            var oldOrder = orderRep.GetOrderByNumberAndProvider(orderNumber, providerId);
            foreach (OrderItemWeb item in orderWeb.OrderItemList)
            {
                if (item.Name == orderWeb.Number)
                {
                    return "Имя продукта не должно совпадать с номером заказа";
                }
            }
            if (orderWeb.Number != orderNumber && orderWeb.ProviderId != providerId &&  orderRep.GetOrderByNumberAndProvider(orderWeb.Number, orderWeb.ProviderId) != null)
            {
                return "Такой заказ от этого производителя уже есть";
            }
            orderRep.UpdateOrder(oldOrder.Id, orderWeb);
            orderItemWebRep.UpdateAll(oldOrder.Id, orderWeb.OrderItemList);
            return "true";
        }
    }
}