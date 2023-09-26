using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace _3amStoreAPI.Service
{
    public class OrderServiceImp : IOrderRepository
    {
        private readonly DatabaseContext _dbContext;

        public OrderServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderModel> AddOrder(OrderModel Order)
        {
            try
            {
                OrderModel order = await _dbContext.Orders.FirstOrDefaultAsync(e => e.order_id.Equals(Order.order_id));
                if (order == null)
                {
                    await _dbContext.Orders.AddAsync(Order);
                    await _dbContext.SaveChangesAsync();
                    return Order;
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

        public async Task<bool> DeleteOrder(int Order_id)
        {
            OrderModel order = await _dbContext.Orders.FirstOrDefaultAsync(e => e.order_id.Equals(Order_id));
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<OrderModel>> GetOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<OrderModel> GetOrderById(int Order_id)
        {
            OrderModel order = await _dbContext.Orders.FirstOrDefaultAsync(e => e.order_id.Equals(Order_id));
            if (order != null)
            {
                return order;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByUserId(int User_id)
        {
            var order_list = await _dbContext.Orders.Where(e => e.user_id.Equals(User_id)).ToListAsync();
            if (order_list != null)
            {
                return order_list;
            }
            else
            {
                return null;
            }
        }

        public async Task<OrderModel> UpdateOrder(OrderModel Order)
        {

            OrderModel order = await _dbContext.Orders.FirstOrDefaultAsync(e => e.order_id.Equals(Order.order_id));
            if (order != null)
            {
                _dbContext.Entry(Order).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return Order;
            }
            else
            {
                return null;
            }
        }
    }
}
