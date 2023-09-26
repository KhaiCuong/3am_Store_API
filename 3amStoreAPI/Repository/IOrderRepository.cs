using _3amStoreAPI.Model;

namespace _3amStoreAPI.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetOrders();
        Task<IEnumerable<OrderModel>> GetOrdersByUserId(int User_id);
        Task<OrderModel> GetOrderById(int Order_id);
        Task<OrderModel> AddOrder(OrderModel Order);
        Task<OrderModel> UpdateOrder(OrderModel Order);
        Task<bool> DeleteOrder(int Order_id);
    }
}
