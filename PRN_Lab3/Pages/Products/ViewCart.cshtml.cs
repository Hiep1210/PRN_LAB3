using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN_Lab3.Logic;
using PRN_Lab3.Models;

namespace PRN_Lab3.Pages.Products
{
    public class ViewCart : PageModel
    {
        public Dictionary<Product, int> Cart { get; set; } = new Dictionary<Product, int>();
        private CartService service;
        private ProductService productService;
        public ViewCart(IHttpContextAccessor httpContext, ProductService pservice)
        {
            service = new CartService(httpContext.HttpContext.Session);
            productService = pservice;
        }
        private void GetData()
        {
            Dictionary<int, int> cart = service.GetCart();
            if(cart == null)
            {
                return;
            }
            foreach (int productId in cart.Keys)
            {
                Product? p = productService.GetProduct(productId);
                if (p != null)
                {
                    Cart.Add(p, cart[productId]);
                }
            }
        }
        public void OnGet()
        {
            GetData();
        }
    }
}
