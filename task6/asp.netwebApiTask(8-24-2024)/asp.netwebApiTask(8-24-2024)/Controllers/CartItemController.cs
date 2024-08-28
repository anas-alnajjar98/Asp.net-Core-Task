using asp.netwebApiTask_8_24_2024_.DTOs;
using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.netwebApiTask_8_24_2024_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly MyDbContext _context;
        public CartItemController(MyDbContext context) 
        {
        _context = context;
        }
        [HttpGet("getAllCartItem")]
        public IActionResult getAllCartItem() {
            var cartItem = _context.CartItems.Select(x =>
            new CartItemRequestDTO {
           CartItemId = x.CartItemId,
           CartId = x.CartId,
           Quantity = x.Quantity,
                Product = new productDTO
                {
                    ProductId = x.Product.ProductId,
                    ProductName = x.Product.ProductName,
                    Price = (int)x.Product.Price,

                }

            }
           
            );
            return Ok( cartItem );
        }
        [HttpPost()]
        public IActionResult AddCartItem([FromBody] AddCartItemRequestDTO newCart) {

            var cartItem = new CartItem {
                CartId = newCart.CartId,
                ProductId = newCart.ProductId,
                Quantity = newCart.Quantity,
            };
            _context.CartItems.Add( cartItem );
            _context.SaveChanges();
            return Ok( cartItem );
        }
    }
}
