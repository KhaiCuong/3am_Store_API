using _3amStoreAPI.Model;

namespace _3amStoreAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetCategories();
        Task<CategoryModel> GetCategoryById(string Category_id);
        Task<CategoryModel> AddCategory(CategoryModel Category);
        Task<CategoryModel> UpdateCategory(CategoryModel Category);
        Task<bool> UpdateStatusCategory(string Category_id);
        Task<bool> DeleteCategory(string Category_id);
    }
}
