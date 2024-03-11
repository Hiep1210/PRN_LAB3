using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN_Lab3.Logic;
using PRN_Lab3.Models;

namespace PRN_Lab3.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private ProductService productService;
        [BindProperty]
        public int PageIndex { get; set; } = 1;
        [BindProperty]
        public int TotalPage { get; set; }
        [BindProperty]
        public string ShipCity { get; set; }
        private readonly int pagesize = 10;
        private OrderService orderService;
        public List<String> Cities { get; set; }
        public IndexModel(ProductService productService, OrderService orderService)
        {
            this.productService = productService;
            this.orderService = orderService;

        }

        public List<Order> Order { get;set; } = default!;

        public void OnGet()
        {
            FetchData();
        }

        private void FetchData()
        {
            Cities = orderService.GetShipCity();
            var filterList = orderService.GetOrdersByFilter(ShipCity);
            TotalPage = (int)Math.Ceiling((double)filterList.Count() / pagesize);
            Order = productService.GetDataByPage<Order>(filterList, PageIndex, TotalPage);
        }

        public void OnPostFilter()
        {
            FetchData();
        }
    }
}
