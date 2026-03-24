using DoAnCuoiKy.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnCuoiKy.Model;

namespace DoAnCuoiKy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            string cookieValue = Request.Cookies["UserSession"];

            Console.WriteLine("Gia tri Cookie nhan duoc: '" + cookieValue );

            if (string.IsNullOrEmpty(cookieValue))
            {
                return Redirect("/DangNhap");
            }

            return Page();
        }
    }
}
