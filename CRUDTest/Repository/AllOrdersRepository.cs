using CRUDTest.Interfaces;
using CRUDTest.Models;
using System.Linq;

namespace CRUDTest.Repository
{
    public class AllOrdersRepository : IAllOrdersRep
    {
        private IOrderWebRep orderWebRep;
        private IOrderItemWebRep orderItemWebRep;
        private IProviderRep providerRep;
        public AllOrdersRepository(IOrderWebRep orderWebRep, IOrderItemWebRep orderItemWebRep, IProviderRep providerRep) 
        {
            this.orderWebRep = orderWebRep;
            this.orderItemWebRep = orderItemWebRep;
            this.providerRep = providerRep;
        }

        AllOrdersWeb IAllOrdersRep.GetAllOrders()
        {
            AllOrdersWeb allOrdersWeb = new AllOrdersWeb();          
            allOrdersWeb.ProviderList = providerRep.GetProviders.ToList();
            allOrdersWeb.OrderItemNameList = orderItemWebRep.GetAllItemName();
            allOrdersWeb.OrderItemUnitList = orderItemWebRep.GetAllItemUnit();
            allOrdersWeb.timeBeg = DateTime.Now.AddMonths(-1);
            allOrdersWeb.timeEnd = DateTime.Now;
            allOrdersWeb.OrderWebList = orderWebRep.GetOrdersWeb().Where(p => p.Date > allOrdersWeb.timeBeg && p.Date < allOrdersWeb.timeEnd).ToList();
            return allOrdersWeb;
        }
    }
}