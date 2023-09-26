using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3amStoreAPI.Model
{
    public class OrderDetailModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int detail_id { get; set; }      
        [Required]
        [Range(1, 50, ErrorMessage = "Quantity must be between 1 and 50")]
        public int quantity { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "ProductName must be 3-30 characters")]
        public string produc_name { get; set; }
        [Required]
        [Range(1, 100000000000, ErrorMessage = "Price must be greater than 1")]
        public decimal price { get; set; }
        public string image { get; set; }



        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; }

        // Foreign key 
        public string product_id { get; set; }
        public virtual ProductModel? product { get; set; }
        public int order_id { get; set; }
        public virtual OrderModel? order { get; set; }

    }
}
