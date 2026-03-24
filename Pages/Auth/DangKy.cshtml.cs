using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DoAnCuoiKy.Model; // Nhớ kiểm tra lại xem namespace chứa class User có đúng không
using DoAnCuoiKy.Data;  // Nhớ kiểm tra lại namespace chứa MyDbContext

namespace MyApp.Pages
{
    public class DangKyModel : PageModel
    {
        // 1. Gọi Database Context
        private readonly AppDbContext _context;

        public DangKyModel(AppDbContext context)
        {
            _context = context;
        }

        // 2. Ràng buộc dữ liệu từ Form HTML truyền lên
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        // Biến để hiển thị thông báo lỗi (ví dụ: Trùng email)
        public string? ErrorMessage { get; set; }

        // 3. Class InputModel chuyên dùng để nhận dữ liệu Đăng ký
        public class InputModel
        {
            [Required(ErrorMessage = "Please enter your email.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string Email { get; set; } = default!;

            [Required(ErrorMessage = "Please create a password.")]
            [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = default!;
        }

        public void OnGet()
        {
            // Hàm này chạy khi người dùng mới mở trang Đăng ký (không cần làm gì cả)
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Bước 1: Kiểm tra xem người dùng nhập đã qua được các điều kiện ở InputModel chưa
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Bước 2: Kiểm tra xem Email này đã có ai đăng ký trong Database chưa
            var emailExists = await _context.Users.AnyAsync(u => u.Email == Input.Email);

            if (emailExists)
            {
                // Nếu có rồi thì báo lỗi và dừng lại
                ErrorMessage = "This email is already registered. Please use another one or log in.";
                return Page();
            }

            // Bước 3: Tạo đối tượng User mới từ dữ liệu nhập vào
            // LƯU Ý: id sẽ tự động sinh vì trong file User.cs bạn đã viết: public string id { get; set; } = Guid.NewGuid().ToString();
            var newUser = new User
            {
                Email = Input.Email,
                Password = Input.Password
                // Mẹo nhỏ: Trong các dự án thực tế, người ta sẽ không lưu thẳng Password mà sẽ băm (Hash) nó ra cho an toàn. 
                // Nhưng ở mức đồ án thì lưu thẳng thế này vẫn OK.
            };

            // Bước 4: Thêm vào EF Core và lưu xuống SQL Server
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Bước 5: Đăng ký thành công, tự động đá người dùng về trang Đăng nhập
            return Redirect("/DangNhap");
        }
    }
}