using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController (IDutchRepository repository, ILogger<OrdersController> logger)
        {

            _repository = repository;
            _logger = logger;

        }
        //-----------------------------------------------Get Orders
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //-----------------------------------------------Get single order
        [HttpGet("{id:int}")]
        public IActionResult GetOrder(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);

                if (order != null) return Ok(order);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //-----------------------------------------------Post order
        [HttpPost]
        public IActionResult Post([FromBody]Order model)
        {
            try
            {
                _repository.AddEntity(model);
                if (_repository.SaveAll())
                {
                    return Created($"/api/orders/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest("failed to save new order");
        }


    }
}
