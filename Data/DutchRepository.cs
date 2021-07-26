﻿using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


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
            return _ctx.Orders.ToList();
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

    }
}
