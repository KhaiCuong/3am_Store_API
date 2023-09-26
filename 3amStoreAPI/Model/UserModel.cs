using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3amStoreAPI.Model
{
    public class UserModel
    {
        [Key]
        [Required]
        public int user_id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Full Name must be 3-50 characters")]
        public string fullname { get; set; }
        [Required]
        public string phone_number { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string role { get; set; }


        //login
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6-20 characters")]
        public string password { get; set; }



        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime update_at { get; }



        // Tham chiếu đến bảng sẽ nhận "user_id" làm khóa ngoại
        public virtual ICollection<OrderModel>? order { get; set; }
        public virtual ICollection<FeedbackModel>? feedback { get; set; }
    }
}
