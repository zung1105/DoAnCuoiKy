using Microsoft.EntityFrameworkCore;
using DoAnCuoiKy.Model;

namespace DoAnCuoiKy.Data
{
    public class AppDbContext : DbContext
    {
        // Options này được truyền từ Program.cs khi đăng ký AddDbContext
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        // ⚠ RẤT QUAN TRỌNG:
        // OnModelCreating dùng để cấu hình quan hệ giữa các bảng
        // Khi Fluent API cần can thiệp (như sửa cascade delete)


        // DbSet đại diện cho bảng trong database
        // => Tạo bảng Wallets

        // => Tạo bảng Transactions
    }
}
