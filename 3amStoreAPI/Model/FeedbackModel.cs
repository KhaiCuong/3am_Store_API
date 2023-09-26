using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3amStoreAPI.Model
{
    public class FeedbackModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? title { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Start must be between 1 and 5")]
        public int start { get; set; }



        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; } = DateTime.Now;



        // Foreign key 
        public string product_id { get; set; }
        public virtual ProductModel? product { get; set; }
        public int user_id { get; set; }
        public virtual UserModel? user { get; set; }
    }
}
