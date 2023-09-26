using _3amStoreAPI.Repository;
using _3amStoreAPI.CustomStatusCode;
using _3amStoreAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3amStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<OrderDetailModel>>>> GetOrderDetailList()
        {
            try
            {
                var resources = await _orderDetailRepository.GetOrderDetails();
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<OrderDetailModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<OrderDetailModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<OrderDetailModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResult<OrderDetailModel>>> GetOrderDetail(int id)
        {
            try
            {
                var resource = await _orderDetailRepository.GetOrderDetailById(id);
                if (resource != null)
                {
                    var response = new CustomResult<OrderDetailModel>(200, "Get successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<OrderDetailModel>(404, "Resource not Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<OrderDetailModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{order_id}")]
        public async Task<ActionResult<CustomResult<IEnumerable<OrderDetailModel>>>> GetOrderDetailListByOrderId(int order_id)
        {
            try
            {
                var resources = await _orderDetailRepository.GetOrderDetailByOrder(order_id);
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<OrderDetailModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<OrderDetailModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<OrderDetailModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomResult<OrderDetailModel>>> AddOrderDetail(OrderDetailModel OrderDetail)
        {
            try
            {
                var resource = await _orderDetailRepository.AddOrderDetail(OrderDetail);
                if (resource != null)
                {
                    var response = new CustomResult<OrderDetailModel>(200, "Resource Created", OrderDetail, null);
                    return Ok(resource);
                }
                else
                {
                    var response = new CustomResult<OrderDetailModel>(400, "unable to create resource", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<OrderDetailModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<CustomResult<OrderDetailModel>>> UpdateOrderDetail(OrderDetailModel orderDetail, int detail_id)
        {
            try
            {
                var resource = await _orderDetailRepository.GetOrderDetailById(detail_id);
                if (resource != null)
                {
                    var resourceUpdate = await _orderDetailRepository.UpdateOrderDetail(orderDetail, detail_id);
                    var response = new CustomResult<OrderDetailModel>(200, "Update Successfully", resourceUpdate, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<OrderDetailModel>(400, "Cannot Update", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<OrderDetailModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResult<string>>> DeleteOrderDetail(int id)
        {
            bool resourceDeleted = false;
            var resource = await _orderDetailRepository.GetOrderDetailById(id);

            if (resource != null)
            {
                resourceDeleted = await _orderDetailRepository.DeleteOrderDetail(id);
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
