using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN_Lab3.Hubs;
using PRN_Lab3.Logic;
using PRN_Lab3.Models;

namespace PRN_Lab3.Pages.Products
{
    public class IndexModel : PageModel
    {
        private IHttpContextAccessor _httpContext;
        private ProductService service;
        IHubContext<CartHub> myhub;
        [BindProperty]
        public int PageIndex { get; set; } = 1;
        [BindProperty]
        public int TotalPage {  get; set; }
        [BindProperty]
        public int CategoryId {  get; set; }
        public List<Category> Categories = new List<Category>();
        private readonly int pagesize = 10;
        public IndexModel(ProductService service, IHttpContextAccessor httpContext, IHubContext<CartHub> _hub)
        {
            this.service = service;
            _httpContext = httpContext;
            myhub = _hub;
        }

        public List<Product> Product { get;set; } = default!;

        public void OnGet()
        {
            FetchData();
            
        }
        private void FetchData()
        {
            Categories = service.GetCategories();
            var filterList = service.GetProductsByCate(CategoryId);
            TotalPage = (int)Math.Ceiling((double)filterList.Count() / pagesize);
            Product = service.GetDataByPage<Product>(filterList, PageIndex, TotalPage);
        }

        public async void OnPostAddCart(int? ProductId)
        {
            CartService service = new CartService(_httpContext.HttpContext.Session);
            service.AddToCart(ProductId ?? 0);
            await myhub.Clients.All.SendAsync("CartAdded");
            FetchData();
        }

        public void OnPostFilter()
        {
            FetchData();
        }
    }
}
