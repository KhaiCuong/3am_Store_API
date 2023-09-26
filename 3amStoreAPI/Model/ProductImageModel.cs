using System.ComponentModel.DataAnnotations;

namespace _3amStoreAPI.Model
{
    public class ProductImageModel
    {
        [Key]
        [Required]
        public int image_id { get; set; }
        [Required]
        public string image_url { get; set; }

        // Foreign key 
        public string product_id { get; set; }
        public virtual ProductModel? product { get; set; }

    }
}
