using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace _3amStoreAPI.Service
{
    public class ProductImageServiceImp : IProductImageRepository
    {
        private readonly DatabaseContext _dbContext;

        public ProductImageServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddProductImage(List<IFormFile> files, string Product_id)
        {
            ProductModel product_images = await _dbContext.Products.FindAsync(Product_id);
            if (product_images != null)
            {
                if (files.Count > 0 && files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var fileName = Path.GetRandomFileName() + Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }

                            var image = new ProductImageModel
                            {
                                image_url = "/uploads/" + fileName,
                                product_id = Product_id,
                            };


                            await _dbContext.ProductImages.AddAsync(image);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    // thêm vào cột image trong ProductModel
                    ProductImageModel Img = await _dbContext.ProductImages.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
                    ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
                    if (product != null && Img != null)
                    {
                        product.image = Img.image_url;
                        _dbContext.Products.Update(product);
                        await _dbContext.SaveChangesAsync();

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductImage(string Product_id)
        {
            ProductImageModel product_images = await _dbContext.ProductImages.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
            if (product_images != null)
            {
                var oldImg = await _dbContext.ProductImages.Where(e => e.product_id.Equals(Product_id)).ToListAsync();

                if (oldImg != null)
                {
                    foreach (var image in oldImg)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), image.image_url.TrimStart('/'));
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            _dbContext.ProductImages.Remove(image);
                            await _dbContext.SaveChangesAsync();

                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);

                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductImageModel>> GetProductImages()
        {
            return await _dbContext.ProductImages.ToListAsync();

        }

        public async Task<IEnumerable<string>> GetProductImageByProductId(string Product_id)
        {
            ProductImageModel product_images = await _dbContext.ProductImages.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
            if (product_images != null)
            {
                var oldImg = await _dbContext.ProductImages.Where(e => e.product_id.Equals(Product_id)).ToListAsync();

                if (oldImg != null)
                {
                    List<string> images = new List<string>();
                    string filePath;
                    foreach (var image in oldImg)
                    {
                        filePath = image.image_url.TrimStart('/');
                        images.Add(filePath);

                    }
                    return images;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateProductImage(List<IFormFile> files, string Product_id)
        {
            ProductImageModel product_images = await _dbContext.ProductImages.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
            if (product_images != null)
            {
                var oldImg = await _dbContext.ProductImages.Where(e => e.product_id.Equals(Product_id)).ToListAsync();

                if (oldImg != null)
                {
                    foreach (var image in oldImg)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), image.image_url.TrimStart('/'));
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            _dbContext.ProductImages.Remove(image);
                            await _dbContext.SaveChangesAsync();

                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);

                            }
                        }
                    }
                }

                if (files.Count > 0 && files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var fileName = Path.GetRandomFileName() + Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }

                            var image = new ProductImageModel
                            {
                                image_url = "/uploads/" + fileName,
                                product_id = Product_id,
                            };


                            await _dbContext.ProductImages.AddAsync(image);
                            await _dbContext.SaveChangesAsync();

                        }
                    }

                    // thêm vào cột image trong ProductModel
                    ProductImageModel Img = await _dbContext.ProductImages.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
                    ProductModel product = await _dbContext.Products.FirstOrDefaultAsync(e => e.product_id.Equals(Product_id));
                    if (product != null && Img != null)
                    {
                        product.image = Img.image_url;
                        _dbContext.Products.Update(product);
                        await _dbContext.SaveChangesAsync();

                    }
                    return true;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
