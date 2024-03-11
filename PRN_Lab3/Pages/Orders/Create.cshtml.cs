using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN_Lab3.Logic;
using PRN_Lab3.Models;

namespace PRN_Lab3.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private IHttpContextAccessor _httpContext;
        public Dictionary<int,int> Cart { get; set; }
        private OrderService orderService;
        private CartService cartService;
        public List<string> CountryName { get; set; }
        public CreateModel(OrderService orderService, IHttpContextAccessor httpContext)
        {
            this.orderService = orderService;
            _httpContext = httpContext;
            cartService = new CartService(_httpContext.HttpContext.Session);
            Cart = cartService.GetCart();
            CountryName = orderService.GetAllOrders().Select(x => x.ShipCountry).ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Order == null)
            {
                return Page();
            }
            orderService.AddOrder(Order, Cart);
            return RedirectToPage("./Index");
        }
        [BindProperty]
        public Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        //public async Task<IActionResult> OnPostAsync()
        //{
        //  if (!ModelState.IsValid || _context.Orders == null || Order == null)
        //    {
        //        return Page();
        //    }

        //    _context.Orders.Add(Order);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}
