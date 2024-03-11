using Microsoft.EntityFrameworkCore;
using PRN_Lab3.Models;

namespace PRN_Lab3.Logic
{
    public class OrderService
    {
        private PRN_Lab3.Models.NorthwindContext context;
        private ProductService productService;
        public OrderService(PRN_Lab3.Models.NorthwindContext context, ProductService pService)
        {
            this.context = context;
            this.productService = pService;
        }

        public void AddOrder(Order order, Dictionary<int, int> Cart)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            OrderDetail detail = new OrderDetail();
            detail.OrderId = order.OrderId;
            foreach (int productID in Cart.Keys)
            {
                detail.ProductId = productID;
                detail.Quantity = (short)Cart[productID];
                context.OrderDetails.Add(detail);
            }
            context.SaveChanges();
        }

        public List<Order> GetAllOrders()
        {
            return context.Orders.Include(x => x.OrderDetails).ToList();
        }

        public List<Order> GetOrdersByFilter(string city)
        {
            return context.Orders.Include(x => x.OrderDetails)
                .Where(x => (city == null) || (x.ShipCity == city)).ToList();
        }

        public List<string> GetShipCity()
        {
            return context.Orders.Select(x => x.ShipCity).ToList();
        }
    }
}
