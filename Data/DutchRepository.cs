using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;

        public DutchRepository(DutchContext ctx)
        {
            _ctx = ctx;
        }

        //----------------------------------------------------Get all orders

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders.Include(o => o.Items)
                              .ThenInclude(i => i.Product)
                              .ToList();
        }

        //----------------------------------------------------Get order by id
        public Order GetOrderById(int id)
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        //----------------------------------------------------Post order
        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        //----------------------------------------------------Get all products
        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                       .OrderBy(p => p.Title)
                       .ToList();
        }

        //----------------------------------------------------Get products by category
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                       .Where(p => p.Category == category)
                       .ToList();
        }

        //----------------------------------------------------Save all

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        
    }
}
