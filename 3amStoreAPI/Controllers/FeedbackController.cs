using _3amStoreAPI.CustomStatusCode;
using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3amStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<FeedbackModel>>>> GetFeedbackList()
        {
            try
            {
                var resources = await _feedbackRepository.GetFeedbacks();
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<FeedbackModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<FeedbackModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<FeedbackModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResult<IEnumerable<FeedbackModel>>>> GetFeedbackListByProductId(string id)
        {
            try
            {
                var resources = await _feedbackRepository.GetFeedbacksByProductId(id);
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<FeedbackModel>>(200, "Resources Found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<FeedbackModel>>(404, "No resource Found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<FeedbackModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }



        [HttpPost]
        public async Task<ActionResult<CustomResult<FeedbackModel>>> AddFeedback(FeedbackModel feedback)
        {
            try
            {
                var resource = await _feedbackRepository.AddFeedback(feedback);
                if (resource != null)
                {
                    var response = new CustomResult<FeedbackModel>(200, "Resource Created", feedback, null);
                    return Ok(resource);
                }
                else
                {
                    var response = new CustomResult<FeedbackModel>(400, "unable to create resource", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new CustomResult<FeedbackModel>()
                {
                    Message = "An error occurred while retrieving the model",
                    Error = ex.Message
                });
            }
        }



    }
}
