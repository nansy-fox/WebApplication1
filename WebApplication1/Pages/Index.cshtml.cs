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

        // Метод для обработки GET-запроса (отображение формы)
        public void OnGet()
        {
        }

        // Метод для обработки POST-запроса (проверка логина и пароля)
        public IActionResult OnPost()
        {
            hashPassword hash=new hashPassword();
            string hashedPassword=hash.HashingPassword(Password);
            // Ищем пользователя с таким логином и паролем в базе данных
            var user = _context.Users.FirstOrDefault(u => u.login == Login && u.password == hashedPassword);

            if (user != null)
            {
                // Если пользователь найден, перенаправляем на домашнюю страницу
                TempData["UserId"] = user.id;
                HttpContext.Session.SetInt32("UserId", user.id);
                //return RedirectToPage("/HomePage");
                if (user.role == 1)
                {
                    Console.WriteLine($"UserId из TempData: {TempData["UserId"]}");
                    return RedirectToPage("/HomePage");  // Переходим на главную страницу
                }
                else
                {
                    return RedirectToPage("/ModeratorPage");  // Переходим на страницу модератора
                }

            }
            else
            {
                // Если пользователь не найден, добавляем ошибку в ModelState
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль.");
                return Page(); // Возвращаем ту же страницу с ошибкой
            }
        }

    }
}
