using _3amStoreAPI.Model;

namespace _3amStoreAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetProducts();
        Task<IEnumerable<ProductModel>> GetProductByCategory(string Category_id);
        Task<ProductModel> GetProductById(string Product_id);
        Task<ProductModel> AddProduct(ProductModel Product);
        Task<ProductModel> UpdateProduct(ProductModel Product);
        Task<bool> DeleteProduct(string Product_id);
        Task<bool> UpdateProductInStock(string Product_id, int Quantity);
        Task<bool> UpdateStatusProduct(string Product_id);

    }
}
