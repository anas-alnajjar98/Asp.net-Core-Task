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

        [HttpGet("GetAllItem")]
        public IActionResult GetAllitems()
        {
            var items = _context.CartItems.Select(x => new CartItemRequestDTO
            {

                CartId = x.CartId,
                Quantity = x.Quantity,
                Product = new productDTO
                {
                    ProductId = x.Product.ProductId,
                    ProductName = x.Product.ProductName,
                    Price = x.Product.Price,
                }

            }).ToList();
            return Ok(items);
        }
        [HttpPost()]
        public IActionResult AddCartItem([FromBody] AddCartItemRequestDTO newCart)
        {

            var cartItem = new CartItem
            {
                CartId = newCart.CartId,
                ProductId = newCart.ProductId,
                Quantity = newCart.Quantity,
            };
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            return Ok(cartItem);
        }
        [HttpGet("getCartByID/{id}")]
        public IActionResult GetCartByID(int id)
        {
            var userCart = _context.Carts
                .Where(x => x.UserId == id)
                .Select(x => x.CartItems.Select(p => new CartItemRequestDTO
                {
                   CartItemId=p.CartItemId,
                    CartId = x.CartId,
                    Quantity = p.Quantity,
                    Product = new productDTO
                    {
                        ProductId = p.Product.ProductId,
                        Price = p.Product.Price,
                        ProductName = p.Product.ProductName,
                    }
                }))
                .FirstOrDefault();

            if (userCart == null || !userCart.Any())
            {
                return NotFound("Cart not found for the given user.");
            }

            return Ok(userCart);
        }
        [HttpPut("UpdateCartItemQuantity/{id:int}")]
        public IActionResult UpdateCartItemQuantity(int id, [FromBody] int newQuantity)
        {
           
            var userCart = _context.CartItems.Find(id);

            if (userCart == null)
            {
                return NotFound(); 
            }
            userCart.Quantity = newQuantity;
            _context.SaveChanges();
            return Ok(userCart);
        }
        [HttpDelete("DeletCartItemByID({id:int})")]
        public IActionResult DeleteCartItem(int id) {
            var user = _context.CartItems.FirstOrDefault(x => x.CartItemId == id);
            if (user == null) { NotFound(); }
            _context.CartItems.Remove(user);
            _context.SaveChanges();
            return Ok(user);
        }
       


    }
}
