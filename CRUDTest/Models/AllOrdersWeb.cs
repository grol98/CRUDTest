namespace CRUDTest.Models
{
    public class AllOrdersWeb
    {
        public List<OrderWeb> OrderWebList { get; set; }     // список заказов
        public List<ProviderDB> ProviderList { get; set; }   // список поставщиков
        public List<string> OrderItemNameList { get; set; }  // список наименований товаров
        public List<string> OrderItemUnitList { get; set; }  // список размерностей товаров
        public DateTime timeBeg { get; set; }                // Дата начала выборки
        public DateTime timeEnd { get; set; }                // Дата конца выборки
    }
}
