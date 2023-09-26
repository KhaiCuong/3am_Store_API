using _3amStoreAPI.CustomStatusCode;
using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3amStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<UserModel>>>> GetUserList()
        {
            try
            {
                var resources = await _userRepository.GetUsers();
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<UserModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<UserModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<UserModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResult<UserModel>>> GetUser(int id)
        {
            try
            {
                var resource = await _userRepository.GetUserById(id);
                if (resource != null)
                {
                    var response = new CustomResult<UserModel>(200, "Get successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<UserModel>(404, "Resource not Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<UserModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<CustomResult<UserModel>>> AddUser(UserModel user)
        {
            try
            {
                var resource = await _userRepository.AddUser(user);
                if (resource != null)
                {
                    var response = new CustomResult<UserModel>(200, "Resource Created", user, null);
                    return Ok(resource);
                }
                else
                {
                    var response = new CustomResult<UserModel>(400, "unable to create resource", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<UserModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CustomResult<UserModel>>> UpdateUser(UserModel user)
        {
            try
            {
                var resource = await _userRepository.GetUserById(user.user_id);
                if (resource != null)
                {
                    var resourceUpdate = await _userRepository.UpdateUser(user);
                    var response = new CustomResult<UserModel>(200, "Update Successfully", resourceUpdate, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<UserModel>(400, "Cannot Update", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<UserModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResult<string>>> DeleteUser(int id)
        {
            bool resourceDeleted = false;
            var resource = await _userRepository.GetUserById(id);

            if (resource != null)
            {
                resourceDeleted = await _userRepository.DeleteUser(id);
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
