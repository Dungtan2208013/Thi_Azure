using Microsoft.EntityFrameworkCore;
using WebAppShopping.Models;

namespace WebAppShopping.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public object OrderTbl { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("OrderTbl"); // Thiết lập tên bảng trong cơ sở dữ liệu
                                                              // Các cấu hình khác nếu cần thiết
        }
    }
}

