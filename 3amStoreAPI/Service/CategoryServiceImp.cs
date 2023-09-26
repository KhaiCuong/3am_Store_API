using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace _3amStoreAPI.Service
{
    public class CategoryServiceImp : ICategoryRepository
    {
        private readonly DatabaseContext _dbContext;

        public CategoryServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryModel> AddCategory(CategoryModel Category)
        {
            try
            {
                CategoryModel category = await _dbContext.Categories.FirstOrDefaultAsync(e => e.category_id.Equals(Category.category_id));
                if (category == null)
                {
                    await _dbContext.Categories.AddAsync(Category);
                    await _dbContext.SaveChangesAsync();
                    return Category;
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

        public async Task<bool> DeleteCategory(string Category_id)
        {
            CategoryModel category = await _dbContext.Categories.FirstOrDefaultAsync(e => e.category_id.Equals(Category_id));
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<CategoryModel> GetCategoryById(string Category_id)
        {
            CategoryModel category = await _dbContext.Categories.FirstOrDefaultAsync(e => e.category_id.Equals(Category_id)); ;
            if (category != null)
            {
                return category;
            }
            else
            {
                return null;
            }
        }

        public async Task<CategoryModel> UpdateCategory(CategoryModel Category)
        {
            CategoryModel category = await _dbContext.Categories.FirstOrDefaultAsync(e => e.category_id.Equals(Category.category_id));
            if (category != null)
            {
                _dbContext.Entry(Category).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return Category;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateStatusCategory(string Category_id)
        {
            CategoryModel category = await _dbContext.Categories.FirstOrDefaultAsync(e => e.category_id.Equals(Category_id));
            if (category != null)
            {
                category.status = !category.status;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
