using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3amStoreAPI.Model
{
    public class OrderModel
    {
        [Key]
        public int order_id { get; set; }
        [Required]
        public string phone_number { get; set; }
        [Required]
        public string address { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be 3-50 characters")]
        public string Username { get; set; }




        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; }


        // Foreign key 
        public int user_id { get; set; }
        public virtual UserModel? user { get; set; }


        // Tham chiếu đến bảng sẽ nhận "order_id" làm khóa ngoại
        public virtual ICollection<OrderDetailModel>? detail { get; set; }

    }
}
