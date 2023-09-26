using _3amStoreAPI.Model;

namespace _3amStoreAPI.Repository
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImageModel>> GetProductImages();
        Task<IEnumerable<string>> GetProductImageByProductId(string Product_id);
        Task<bool> AddProductImage(List<IFormFile> files, string Product_id);
        Task<bool> UpdateProductImage(List<IFormFile> files, string Product_id);
        Task<bool> DeleteProductImage(string Product_id);
    }
}
