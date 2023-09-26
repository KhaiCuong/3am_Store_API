

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;

namespace _3amStoreAPI.Model
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // OrderDetailModel - ProductModel - OrderModel
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetailModel>()
            .HasKey(e => new { e.product_id, e.order_id });
            modelBuilder.Entity<OrderDetailModel>()
                .HasOne(e => e.product)
                .WithMany(e => e.detail)
                .HasForeignKey(e => e.product_id);
            modelBuilder.Entity<OrderDetailModel>()
                .HasOne(e => e.order)
                .WithMany(e => e.detail)
                .HasForeignKey(e => e.order_id);  



            // FeedbackModel - ProductModel - UserModel
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FeedbackModel>()
            .HasKey(e => new { e.product_id, e.user_id });
            modelBuilder.Entity<FeedbackModel>()
                .HasOne(e => e.product)
                .WithMany(e => e.feedback)
                .HasForeignKey(e => e.product_id);
            modelBuilder.Entity<FeedbackModel>()
                .HasOne(e => e.user)
                .WithMany(e => e.feedback)
                .HasForeignKey(e => e.user_id);


            // ProductImageModel - ProductModel
            modelBuilder.Entity<ProductImageModel>()
                           .HasOne(e => e.product)
                           .WithMany(e => e.product_image)
                           .HasForeignKey(e => e.product_id);





            // ProductModel - CategoryModel 
            modelBuilder.Entity<ProductModel>()
                       .HasOne(e => e.category)
                       .WithMany(e => e.product)
                       .HasForeignKey(e => e.category_id);




            // OrderModel - UserModel
            modelBuilder.Entity<OrderModel>()
                       .HasOne(e => e.user)
                       .WithMany(e => e.order)
                       .HasForeignKey(e => e.user_id);




        }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductImageModel> ProductImages { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailModel> Details { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }
    
    }
}
