using CRUDTest.Models;
using CRUDTest.Repository;

namespace CRUDTest.Interfaces
{
    public interface IOrderWebRep
    {
        public OrderWeb GetOrderWebById(int id);
        public OrderWeb GetOrderWebByNumberAndProvider(string orderNumber, int ProviderId);
        public List<OrderWeb> GetOrdersWeb();
        public List<OrderWeb> GetOrdersWebByFilter(string orderNumber, string itemName, string itemUnit, string timeBeg, string timeEnd, string providerName);
        public string AddOrder(OrderWeb orderWeb);
        public string EditOrder(OrderWeb orderWeb, string orderNumber, int providerId);
    }
}
