using asp.netwebApiTask_8_24_2024_.Models;

namespace asp.netwebApiTask_8_24_2024_.DTOs
{
    public class productRequestDTO
    {


        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }

    }
}
