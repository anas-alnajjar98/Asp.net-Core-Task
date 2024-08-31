namespace asp.netwebApiTask_8_24_2024_.DTOs
{
    public class AddCartItemRequestDTO
    {
        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
