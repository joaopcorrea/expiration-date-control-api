namespace ExpirationDateControl_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
