using _3amStoreAPI.CustomStatusCode;
using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3amStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<ProductModel>>>> GetProductList()
        {
            try
            {
                var resources = await _productRepository.GetProducts();
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<ProductModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<ProductModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<ProductModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResult<ProductModel>>> GetProduct(string id)
        {
            try
            {
                var resource = await _productRepository.GetProductById(id);
                if (resource != null)
                {
                    var response = new CustomResult<ProductModel>(200, "Get successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<ProductModel>(404, "Resource not Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<ProductModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{category_id}")]
        public async Task<ActionResult<CustomResult<IEnumerable<ProductModel>>>> GetProductListByCategoryId(string category_id)
        {
            try
            {
                var resources = await _productRepository.GetProductByCategory(category_id);
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<ProductModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<ProductModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<ProductModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomResult<ProductModel>>> AddProduct(ProductModel product)
        {
            try
            {
                var resource = await _productRepository.AddProduct(product);
                if (resource != null)
                {
                    var response = new CustomResult<ProductModel>(200, "Resource Created", product, null);
                    return Ok(resource);
                }
                else
                {
                    var response = new CustomResult<ProductModel>(400, "unable to create resource", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<ProductModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CustomResult<ProductModel>>> UpdateProduct(ProductModel product)
        {
            try
            {
                var resource = await _productRepository.GetProductById(product.product_id);
                if (resource != null)
                {
                    var resourceUpdate = await _productRepository.UpdateProduct(product);
                    var response = new CustomResult<ProductModel>(200, "Update Successfully", resourceUpdate, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<ProductModel>(400, "Cannot Update", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<ProductModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResult<string>>> DeleteProduct(string id)
        {
            bool resourceDeleted = false;
            var resource = await _productRepository.GetProductById(id);

            if (resource != null)
            {
                resourceDeleted = await _productRepository.DeleteProduct(id);
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


        [HttpPut("{id}")]
        public async Task<ActionResult<CustomResult<string>>> UpdateProductInStock(string id, int quantity)
        {
            bool resourceUpdated = false;
            var resource = await _productRepository.GetProductById(id);

            if (resource != null)
            {
                resourceUpdated = await _productRepository.UpdateProductInStock(id,quantity);
            }
            if (resourceUpdated)
            {
                var response = new CustomResult<string>(200, "Resource Update successfully", null, null);
                return Ok(response);
            }
            else
            {
                var response = new CustomResult<string>(400, "Resource not found or unable to Update", null, null);
                return NotFound(response);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CustomResult<string>>> UpdateStatusProduct(string id)
        {
            bool resourceUpdated = false;
            var resource = await _productRepository.GetProductById(id);

            if (resource != null)
            {
                resourceUpdated = await _productRepository.UpdateStatusProduct(id);
            }
            if (resourceUpdated)
            {
                var response = new CustomResult<string>(200, "Resource Update successfully", null, null);
                return Ok(response);
            }
            else
            {
                var response = new CustomResult<string>(400, "Resource not found or unable to Update", null, null);
                return NotFound(response);
            }
        }
    }
}
