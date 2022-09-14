namespace ExpirationDateControl_API.Dtos
{
    public class ProductDto
    {
        public string BarCode { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
