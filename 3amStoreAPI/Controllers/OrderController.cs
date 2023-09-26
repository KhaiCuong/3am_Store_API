using _3amStoreAPI.CustomStatusCode;
using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3amStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<OrderModel>>>> GetOrderList()
        {
            try
            {
                var resources = await _orderRepository.GetOrders();
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<OrderModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<OrderModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<OrderModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResult<OrderModel>>> GetOrder(int id)
        {
            try
            {
                var resource = await _orderRepository.GetOrderById(id);
                if (resource != null)
                {
                    var response = new CustomResult<OrderModel>(200, "Get successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<OrderModel>(404, "Resource not Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<OrderModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{user_id}")]
        public async Task<ActionResult<CustomResult<IEnumerable<OrderModel>>>> GetOrderListByUserId(int user_id)
        {
            try
            {
                var resources = await _orderRepository.GetOrdersByUserId(user_id);
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<OrderModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<OrderModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<OrderModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomResult<OrderModel>>> AddOrder(OrderModel order)
        {
            try
            {
                var resource = await _orderRepository.AddOrder(order);
                if (resource != null)
                {
                    var response = new CustomResult<OrderModel>(200, "Resource Created", order, null);
                    return Ok(resource);
                }
                else
                {
                    var response = new CustomResult<OrderModel>(400, "unable to create resource", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<OrderModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CustomResult<OrderModel>>> UpdateOrder(OrderModel order)
        {
            try
            {
                var resource = await _orderRepository.GetOrderById(order.order_id);
                if (resource != null)
                {
                    var resourceUpdate = await _orderRepository.UpdateOrder(order);
                    var response = new CustomResult<OrderModel>(200, "Update Successfully", resourceUpdate, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<OrderModel>(400, "Cannot Update", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<OrderModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResult<string>>> DeleteOrder(int id)
        {
            bool resourceDeleted = false;
            var resource = await _orderRepository.GetOrderById(id);

            if (resource != null)
            {
                resourceDeleted = await _orderRepository.DeleteOrder(id);
            }
            if (resourceDeleted)
            {
                var response = new CustomResult<string>(200, "Resource deleted successfully", null, null);
                return Ok(response);
            }
            else
            {
                var response = new CustomResult<string>(400, "Resource not found or unable to delete", null, null);
                return NotFound(response);
            }
        }


    }
}
