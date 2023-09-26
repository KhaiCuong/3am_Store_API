using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace _3amStoreAPI.Service
{
    public class ProductServiceImp : IProductRepository
    {
        private readonly DatabaseContext _dbContext;

        public ProductServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductModel> AddProduct(ProductModel Product)
        {
            ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(e => e.product_id.Equals(Product.product_id));
            if (product == null)
            {
                await _dbContext.Products.AddAsync(Product);
                await _dbContext.SaveChangesAsync();
                return Product;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteProduct(string Product_id)
        {
            ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductModel>> GetProductByCategory(string Category_id)
        {
            var product_list = await _dbContext.Products.Where(e => e.category_id.Equals(Category_id)).ToListAsync();
            if (product_list != null)
            {
                return product_list;
            }
            else
            {
                return null;
            }
        }

        public async Task<ProductModel> GetProductById(string Product_id)
        {
            ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<ProductModel> UpdateProduct(ProductModel Product)
        {
            ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(e => e.product_id.Equals(Product.product_id));
            if (product != null)
            {
                _dbContext.Entry(Product).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return Product;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateProductInStock(string Product_id, int Quantity)
        {
            ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
            if (product != null)
            {
                product.instock = product.instock - Quantity;
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateStatusProduct(string Product_id)
        {
            ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
            if (product != null)
            {
                product.status = !product.status;
                _dbContext.Products.Update(product);
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
