using _3amStoreAPI.Model;

namespace _3amStoreAPI.Repository
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<FeedbackModel>> GetFeedbacks();
        Task<IEnumerable<FeedbackModel>> GetFeedbacksByProductId(string Product_id);
        Task<FeedbackModel> AddFeedback(FeedbackModel Feedback);
        //Task<FeedbackModel> UpdateFeedback(FeedbackModel Feedback);
        //Task<bool> DeleteFeedback(int Feedback_id);
    }
}
