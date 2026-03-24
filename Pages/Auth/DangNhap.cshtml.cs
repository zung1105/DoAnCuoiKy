using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DoAnCuoiKy.Model;
using DoAnCuoiKy.Data;
namespace MyApp.Pages
{
    public class DangNhapModel : PageModel
    {
        private readonly AppDbContext _context;

        public DangNhapModel(AppDbContext context)
        {
            _context = context;
        }

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




        public IActionResult OnPost()
        {
            Console.WriteLine(Input.Email);
            Console.WriteLine(Input.Password);
            var email = Input.Email;
            var password = Input.Password;
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                var session = new UserSessionInfo();
                session.SessionID = DateTime.Now.Ticks.ToString(); // Tạo SessionID đơn giản bằng ticks
                session.UserID = user.id;
                string jsonValue = System.Text.Json.JsonSerializer.Serialize(session);
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddSeconds(300),
                    HttpOnly = false,
                    Secure = false,
                };
                Response.Cookies.Append("UserSession", jsonValue, options);
                //return new JsonResult(user);
                return Redirect("/Index");
            }
            else
            {
                return Redirect("/DangNhap");
            }
        }


    }
}