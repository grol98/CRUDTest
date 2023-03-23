namespace CRUDTest.Models
{
    public class OrderItemWeb
    {
        public string Name { get; set; } 
        public decimal Quantity { get; set; } 
        public string Unit { get; set; }

        public OrderItemWeb() { }

        public OrderItemWeb(string Name, decimal Quantity, string Unit)
        {
            this.Name = Name;
            this.Quantity = Quantity;
            this.Unit = Unit;
        }
    }
}
