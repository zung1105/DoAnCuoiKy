using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Pages
{
    public class DangNhapModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string ErrorMessage { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "Vui lòng nhập Email hoặc Tên đăng nhập.")]
            [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập Mật khẩu.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
            // Xóa thông báo lỗi cũ nếu có
            ErrorMessage = string.Empty;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //TODO: Chọc vào CSDL, Viết phần logic kiểm tra thông tin đăng nhập ở đây
            //TODO: Trả về cookie auth hoặc thông tin auth. Redirect về trang chủ nếu đăng nhập thành công, ngược lại trả về lỗi

            // Logic xử lý đăng nhập ở đây
            return RedirectToPage("/Index");
        }
    }
}