using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class HomePageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HomePageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Good> Goods { get; set; }
        public IList<Order> Orders { get; set; }

        // Метод для загрузки товаров и заказов пользователя
        public async Task OnGetAsync()
        {
            Goods = await _context.Goods.ToListAsync();

            // Получаем UserId из сессии
            var userId = HttpContext.Session.GetInt32("user_id");

            if (userId.HasValue)
            {
                // Извлекаем заказы для этого пользователя
                Orders = await _context.Orders
                    .Where(o => o.user_id == userId.Value)
                    .Include(o => o.Goods) // Включаем информацию о товаре
                    .ToListAsync();
            }
        }

        // Метод для создания заказа
        public async Task<IActionResult> OnPostCreateOrderAsync(int goodsId, int quantity)
        {
            // Получаем UserId из сессии
            var userId = HttpContext.Session.GetInt32("user_id");

            if (userId.HasValue)
            {
                // Проверяем, существует ли товар и есть ли он в наличии
                var goods = await _context.Goods.FindAsync(goodsId);
                if (goods != null && goods.in_stock && quantity > 0)
                {
                    // Создаем заказ
                    var order = new Order
                    {
                        user_id = userId.Value,  // Используем UserId как int
                        goods_id = goods.Id,
                        count = quantity
                    };

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Заказ успешно добавлен!";
                    return RedirectToPage();
                }

                // Если товар не найден или его нет в наличии (для отладки)
                TempData["Error"] = "Ошибка при создании заказа.";
                return RedirectToPage();
            }

            // Если UserId не найден в сессии (для отладки)
            TempData["Error"] = "Пользователь не найден.";
            return RedirectToPage();
        }


    }
}
