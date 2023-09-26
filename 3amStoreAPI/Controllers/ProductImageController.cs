using _3amStoreAPI.CustomStatusCode;
using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3amStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageController(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<ProductImageModel>>>> GetProductImageList()
        {
            try
            {
                var resource = await _productImageRepository.GetProductImages();
                if (resource == null)
                {
                    var response = new CustomResult<IEnumerable<ProductImageModel>>(404,
                        "Resource not found or unable to delete", null, null);
                    return NotFound(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<ProductImageModel>>(200,
                        "Get All Product Image successfully", resource, null);

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<IEnumerable<string>>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = ex.Message
                });
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResult<IEnumerable<string>>>> GetProductImagesByProductId(string id)
        {
            try
            {
                var resource = await _productImageRepository.GetProductImageByProductId(id);
                if (resource == null)
                {
                    var response = new CustomResult<IEnumerable<string>>(404,
                        "Resource not found or unable to delete", null, null);
                    return NotFound(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<string>>(200,
                        "Get Product Image successfully", resource, null);

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<IEnumerable<string>>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = ex.Message
                });
            }


        }


        [HttpPost("{id}")]
        public async Task<ActionResult<CustomResult<bool>>> UpdateProductImagesByProductId(List<IFormFile> files, string id)
        {
            try
            {
                var resources = await _productImageRepository.UpdateProductImage(files, id);
                if (resources)
                {
                    var response = new CustomResult<bool>(200, "Resource created", true, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<bool>(404, "No Resources Found", false, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<IEnumerable<string>>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<CustomResult<bool>>> AddProductImages(List<IFormFile> files, string Product_Id)
        {
            try
            {
                var resources = await _productImageRepository.AddProductImage(files, Product_Id);
                if (resources)
                {
                    var response = new CustomResult<bool>(200, "Resource created", true, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<bool>(404, "Not Resources Found", false, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<IEnumerable<string>>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResult<bool>>> DeleteProductImages(string id)
        {
            var resource = await _productImageRepository.DeleteProductImage(id);
            if (resource != null)
            {

                var response = new CustomResult<bool>(200,
                    "Resource deleted successfully", true, null);
                return Ok(response);
            }
            else
            {
                var response = new CustomResult<bool>(400,
                    "Resource not found or unable to delete", false, null);
                return NotFound(response);
            }

        }
    }
}
