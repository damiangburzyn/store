using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.ViewModel;
using Store.Services;
using AutoMapper;
using Microsoft.Extensions.Options;
using Store.Contracts;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : BaseController
    {
        private ShoppingCartService _shoppingCartService;

        public ShoppingCartController(IOptions<AppSettings> settings, ILocalPageData pageData, ShoppingCartService cartService, IMapper mapper, ILogger<ShoppingCartController> logger)
            : base(settings, pageData, mapper, logger)
        {
            _shoppingCartService = cartService;
        }



        public ActionResult Index()
        {
            var cart = _shoppingCartService.GetCart(this.HttpContext);

            //Todo: Automapper
            var cartItemsDb = cart.GetCartItems();
            var cartItemsVm = Mapper.Map<List<CartViewModel>>(cartItemsDb);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cartItemsVm,
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return Ok(viewModel);
        }

  
        [HttpPost("{id}/{count}")]
        public ActionResult AddToCart(long id, int count =1)
        {
            _shoppingCartService.AddToCard(id, this.HttpContext, count);
            // Go back to the main store page for more shopping

            return CartSummary();
        }


        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpDelete("{id}")]
        public ActionResult RemoveFromCart(long id)
        {
            var results = _shoppingCartService.RemoveFromCart(id,  this.HttpContext);

           
            return CartSummary();
        }


        //
        // GET: api/shoppingcart/count
       [HttpGet("count")]
        public ActionResult CartSummary()
        {
            var cardCount = _shoppingCartService.CartSummary(this.HttpContext);
         
            return Ok(cardCount);
        }


    }
}