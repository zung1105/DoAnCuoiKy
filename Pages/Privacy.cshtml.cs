using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoAnCuoiKy.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            string cookieValue = Request.Cookies["UserSession"];

            Console.WriteLine("Gia tri Cookie nhan duoc: '" + cookieValue);

            if (string.IsNullOrEmpty(cookieValue))
            {
                return Redirect("/DangNhap");
            }

            return Page();
        }
    }

}
