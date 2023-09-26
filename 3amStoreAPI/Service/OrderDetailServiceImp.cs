using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace _3amStoreAPI.Service
{
    public class OrderDetailServiceImp : IOrderDetailRepository
    {
        private readonly DatabaseContext _dbContext;

        public OrderDetailServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDetailModel> AddOrderDetail(OrderDetailModel OrderDetail)
        {
            try
            {
                OrderModel detail = await _dbContext.Orders.FirstOrDefaultAsync(e => e.order_id.Equals(OrderDetail.order_id));
                if (detail != null)
                {
                    await _dbContext.Details.AddAsync(OrderDetail);
                    await _dbContext.SaveChangesAsync();
                    return OrderDetail;
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

        public async Task<bool> DeleteOrderDetail(int OrderDetail_id)
        {
            OrderDetailModel detail = await _dbContext.Details.FirstOrDefaultAsync(e => e.detail_id.Equals(OrderDetail_id));
            if (detail != null)
            {
                _dbContext.Details.Remove(detail);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<OrderDetailModel> GetOrderDetailById(int OrderDetail_id)
        {
            OrderDetailModel detail = await _dbContext.Details.FirstOrDefaultAsync(e => e.detail_id.Equals(OrderDetail_id));
            if (detail != null)
            {
                return detail;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<OrderDetailModel>> GetOrderDetailByOrder(int Order_id)
        {
            var detail_list = await _dbContext.Details.Where(e => e.order_id.Equals(Order_id)).ToListAsync();
            if (detail_list != null)
            {
                return detail_list;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<OrderDetailModel>> GetOrderDetails()
        {
            return await _dbContext.Details.ToListAsync();
        }

        public async Task<OrderDetailModel> UpdateOrderDetail(OrderDetailModel OrderDetail, int detail_id)
        {
            OrderDetailModel detail = await _dbContext.Details.FirstOrDefaultAsync(e => e.detail_id.Equals(detail_id));
            if (detail != null)
            {
                //_dbContext.Entry(OrderDetail).State = EntityState.Modified;
                _dbContext.Details.Remove(detail);
                await _dbContext.Details.AddAsync(OrderDetail);
                await _dbContext.SaveChangesAsync();
                return OrderDetail;
            }
            else
            {
                return null;
            }
        }
    }
}
