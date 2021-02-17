using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.DTOs;
using Ordering.Core.Repositories;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrdersByUsername(string username)
        {
            var orders = await _orderRepository.GetOrdersByUsername(username);
            if (orders != null) return Ok(_mapper.Map<IEnumerable<OrderResponse>>(orders));
            return NotFound();
        }
    }
}