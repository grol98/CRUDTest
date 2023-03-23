using CRUDTest.Models;
using CRUDTest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.Extensions.Primitives;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace CRUDTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IOrderRep orderRep;
        private IOrderWebRep orderWebRep;
        private IAllOrdersRep allOrdersRep;
        private IProviderRep providerRep;

        private ApplicationContext dbcontext;


        public HomeController(ILogger<HomeController> logger, IOrderRep orderRep, IOrderWebRep orderWebRep, IAllOrdersRep allOrdersRep, IProviderRep providerRep, ApplicationContext context)
        {
            _logger = logger;
            this.orderRep = orderRep;
            this.orderWebRep = orderWebRep;
            this.allOrdersRep = allOrdersRep;
            this.providerRep = providerRep;
            dbcontext = context;
        }


        public IActionResult Index()
        {
            return View(allOrdersRep.GetAllOrders());
        }


        [HttpGet]
        public IActionResult FilterOrder(string orderNumber, string itemName, string itemUnit, string timeBeg, string timeEnd, string providerName)
        {
            var ordersWeb = orderWebRep.GetOrdersWebByFilter(orderNumber, itemName, itemUnit, timeBeg, timeEnd, providerName);
            AllOrdersWeb allOrdersWeb = new AllOrdersWeb();
            allOrdersWeb.OrderWebList = ordersWeb;
            allOrdersWeb.ProviderList = providerRep.GetProviders.ToList();
            return PartialView(allOrdersWeb);
        }


        [HttpGet]
        public IActionResult ShowOrder(string orderNumber, int ProviderId)
        {
            OrderWeb orderWeb = orderWebRep.GetOrderWebByNumberAndProvider(orderNumber, ProviderId);
            ViewData["Provider"] = providerRep.GetProviderById(orderWeb.ProviderId).Name;
            return PartialView(orderWeb);
        }


        [HttpGet]
        public IActionResult EditOrderForm(string number, int providerId)
        {
            EditOrderClass editOrder = new EditOrderClass();
            editOrder.orderWeb = orderWebRep.GetOrderWebByNumberAndProvider(number, providerId);
            editOrder.ProviderList = providerRep.GetProviders.ToList();
            return View(editOrder);
        }

        [HttpGet]
        public IActionResult AddOrderForm()
        {
            return View(providerRep.GetProviders.ToList());
        }

        [HttpPost]
        public IActionResult EditOrder(IFormCollection form, string orderNumber, int providerId)
        {
            OrderWeb orderWeb = ParseForm(form);
            string ans = orderWebRep.EditOrder(orderWeb, orderNumber, providerId);
            return Content(ans);
        }

        [HttpGet]
        public IActionResult RemoveOrder(string number, int providerId)
        {
            _logger.LogWarning(number);
            orderRep.Remove(number, providerId);
            return Redirect("Index");
        }

        [HttpPost]
        [ActionName("NewOrder")]
        public IActionResult AddOrder(IFormCollection form)
        {
            OrderWeb orderWeb = ParseForm(form);
            string ans = orderWebRep.AddOrder(orderWeb);
            return Content(ans);
        }

        [NonAction]
        public OrderWeb ParseForm(IFormCollection form)
        {           
            string[] formArray = form["form"].ToString().Split(new char[] { '&' });
            OrderWeb orderWeb = new OrderWeb();
            orderWeb.Number = formArray[0].Substring(12);            
            orderWeb.ProviderId = int.Parse(formArray[1].Substring(9));
            orderWeb.Date = DateTime.ParseExact(formArray[2].Substring(5).Replace('T', ' ').Replace("%3A", ":"), "yyyy-MM-dd HH:mm", null);
            List <OrderItemWeb> orderItemWebs = new List<OrderItemWeb>();
            for (int i = 3; i < formArray.Length; i += 3)
            {
                _logger.LogInformation(formArray[i + 1].Substring(9));
                OrderItemWeb orderItemWeb = new OrderItemWeb(formArray[i].Substring(5), Convert.ToDecimal(formArray[i + 1].Substring(9).Replace('.', ',')), formArray[i + 2].Substring(5));
                orderItemWebs.Add(orderItemWeb);
            }
            orderWeb.OrderItemList = orderItemWebs;
            return orderWeb;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}