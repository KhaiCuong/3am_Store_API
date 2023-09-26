using _3amStoreAPI.CustomStatusCode;
using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3amStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<CategoryModel>>>> GetCategoryList()
        {
            try
            {
                var resources = await _categoryRepository.GetCategories();
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<CategoryModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<CategoryModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<CategoryModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResult<CategoryModel>>> GetCategory(string id)
        {
            try
            {
                var resource = await _categoryRepository.GetCategoryById(id);
                if (resource != null)
                {
                    var response = new CustomResult<CategoryModel>(200, "Get successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<CategoryModel>(404, "Resource not Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<CategoryModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<CustomResult<CategoryModel>>> AddCategory(CategoryModel category)
        {
            try
            {
                var resource = await _categoryRepository.AddCategory(category);
                if (resource != null)
                {
                    var response = new CustomResult<CategoryModel>(200, "Resource Created", category, null);
                    return CreatedAtAction(nameof(GetCategory), new { id = category.category_id }, category);
                }
                else
                {
                    var response = new CustomResult<CategoryModel>(400, "unable to create resource", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<CategoryModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CustomResult<CategoryModel>>> UpdateCategory(CategoryModel category)
        {
            try
            {
                var resource = await _categoryRepository.GetCategoryById(category.category_id);
                if (resource != null)
                {
                    var resourceUpdate = await _categoryRepository.UpdateCategory(category);
                    var response = new CustomResult<CategoryModel>(200, "Update Successfully", resourceUpdate, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<CategoryModel>(400, "Cannot Update", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<CategoryModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResult<string>>> DeleteCategory(string id)
        {
            bool resourceDeleted = false;
            var resource = await _categoryRepository.GetCategoryById(id);

            if (resource != null)
            {
                resourceDeleted = await _categoryRepository.DeleteCategory(id);
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
