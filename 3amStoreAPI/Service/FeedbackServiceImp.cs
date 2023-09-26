using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace _3amStoreAPI.Service
{
    public class FeedbackServiceImp : IFeedbackRepository
    {

        private readonly DatabaseContext _dbContext;

        public FeedbackServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FeedbackModel> AddFeedback(FeedbackModel Feedback)
        {
            try
            {
                FeedbackModel feedback = await _dbContext.Feedbacks.FirstOrDefaultAsync(e => e.id.Equals(Feedback.id));
                if (feedback == null)
                {
                    await _dbContext.Feedbacks.AddAsync(Feedback);
                    await _dbContext.SaveChangesAsync();
                    return Feedback;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<FeedbackModel>> GetFeedbacks()
        {
            return await _dbContext.Feedbacks.ToListAsync();
        }

        public async Task<IEnumerable<FeedbackModel>> GetFeedbacksByProductId(string Product_id)
        {

            var feedback_list = await _dbContext.Feedbacks.Where(e => e.product_id.Equals(Product_id)).ToListAsync();
            if (feedback_list != null)
            {
                return feedback_list;
            }
            else
            {
                return null;
            }
        }
    }
}
