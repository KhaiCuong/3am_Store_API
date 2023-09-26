using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace _3amStoreAPI.Model
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "ProductId must be 3-20 characters")]
        public string product_id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "ProductName must be 3-30 characters")]
        public string produc_name { get; set; }
        [Required]
        [Range(1, 100000000000, ErrorMessage = "Quantity must be greater than 1")]
        public int instock { get; set; }
        [Required]
        [Range(1, 100000000000, ErrorMessage = "Price must be greater than 1")]
        public decimal price { get; set; }
        [DefaultValue(true)]
        public bool status { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime update_at { get; }



        // Foreign key 
        public string category_id { get; set; }
        public virtual CategoryModel? category { get; set; }


        // Tham chiếu đến bảng sẽ nhận "product_id" làm khóa ngoại
        public virtual ICollection<ProductImageModel>? product_image { get; set; }
        public virtual ICollection<OrderDetailModel>? detail { get; set; }
        public virtual ICollection<FeedbackModel>? feedback { get; set; }


    }
}
