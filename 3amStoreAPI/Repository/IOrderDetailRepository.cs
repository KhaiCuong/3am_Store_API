using _3amStoreAPI.Model;

namespace _3amStoreAPI.Repository
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetailModel>> GetOrderDetails();
        Task<IEnumerable<OrderDetailModel>> GetOrderDetailByOrder(int Order_id);
        Task<OrderDetailModel> GetOrderDetailById(int OrderDetail_id);
        Task<OrderDetailModel> AddOrderDetail(OrderDetailModel OrderDetail);
        Task<OrderDetailModel> UpdateOrderDetail(OrderDetailModel OrderDetail, int detail_id);
        Task<bool> DeleteOrderDetail(int OrderDetail_id);
    }
}
