using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ����� ��� ��������� GET-������� (����������� �����)
        public void OnGet()
        {
        }

        // ����� ��� ��������� POST-������� (�������� ������ � ������)
        public IActionResult OnPost()
        {
            hashPassword hash=new hashPassword();
            string hashedPassword=hash.HashingPassword(Password);
            // ���� ������������ � ����� ������� � ������� � ���� ������
            var user = _context.Users.FirstOrDefault(u => u.login == Login && u.password == hashedPassword);

            if (user != null)
            {
                // ���� ������������ ������, �������������� �� �������� ��������
                TempData["UserId"] = user.id;
                HttpContext.Session.SetInt32("UserId", user.id);
                //return RedirectToPage("/HomePage");
                if (user.role == 1)
                {
                    Console.WriteLine($"UserId �� TempData: {TempData["UserId"]}");
                    return RedirectToPage("/HomePage");  // ��������� �� ������� ��������
                }
                else
                {
                    return RedirectToPage("/ModeratorPage");  // ��������� �� �������� ����������
                }

            }
            else
            {
                // ���� ������������ �� ������, ��������� ������ � ModelState
                ModelState.AddModelError(string.Empty, "������������ ����� ��� ������.");
                return Page(); // ���������� �� �� �������� � �������
            }
        }

    }
}
