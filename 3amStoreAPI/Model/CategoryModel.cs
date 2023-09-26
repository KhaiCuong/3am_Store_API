using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace _3amStoreAPI.Model
{
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "ProductId must be 3-20 characters")]
        public string category_id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "ProductName must be 3-30 characters")]
        public string category_name { get; set; }
        [DefaultValue(true)]
        public bool status { get; set; }
        public string? description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; } = DateTime.UtcNow;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime update_at { get; } = DateTime.UtcNow;


        // Tham chiếu đến bảng sẽ nhận "category_id" làm khóa ngoại
        public virtual ICollection<ProductModel>? product { get; set; }

    }
}
