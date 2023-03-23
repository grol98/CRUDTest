namespace CRUDTest.Models
{
    public class OrderWeb
    {
        public string Number { get; set; }                     // Номер заказа
        public DateTime Date { get; set; }                     // Дата забора заказа
        public int ProviderId { get; set; }                    // Id поставщика
        public List<OrderItemWeb> OrderItemList { get; set; }   // список элементов заказа
    }
}
