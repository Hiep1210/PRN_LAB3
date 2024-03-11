using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PRN_Lab3.Models;
using PRN_Lab3.Pages;
using static NuGet.Packaging.PackagingConstants;

namespace PRN_Lab3.Logic
{
    public class ProductService
    {
        private readonly PRN_Lab3.Models.NorthwindContext context;
        private List<Product> Product;
        private List<Category> Categories;
        private readonly int pagesize = 10;
        public ProductService(PRN_Lab3.Models.NorthwindContext _context)
        {
            this.context = _context;
        }

        public List<Product> GetProducts()
        {
            if (context.Products != null)
            {
                Product = context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToList();
            }
            return Product;
        }

        public List<Category> GetCategories()
        {
            if (context.Categories != null)
            {
                Categories = context.Categories.ToList();
            }
            return Categories;
        }

        public Product? GetProduct(int id)
        {
            Product? p = GetProducts().FirstOrDefault(item => item.ProductId == id);
            return p;
        }

        public List<Product> GetProductsByCate(int categoryID)
        {
            Product = context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(x => (categoryID == 0) || (x.CategoryId == categoryID)).ToList();
            return Product;
        }

        public List<T> GetDataByPage<T>(List<T> product, int PageIndex, int TotalPage)
        {
            var _totalItem = product.Count();
            

            if (TotalPage > 0)
            {
                if (PageIndex > TotalPage) PageIndex = TotalPage;
                //_startIndex = (PageIndex - 1) * _pageSize + 1;

                product = product.Skip((PageIndex - 1) * pagesize)
                    .Take(pagesize)
                    .ToList();
            }
            return product;
        }
    }
}
