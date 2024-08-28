namespace asp.netwebApiTask_8_24_2024_.DTOs
{
    public class CartItemRequestDTO
    {
        public int CartItemId { get; set; }
        public int? CartId { get; set; }
        public int Quantity { get; set; }
        public productDTO Product { get; set; }
    }
    public class productDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
    }
}
