using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
    public class ModeratorPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ModeratorPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Users> Users { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Good> Goods { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
            Orders = await _context.Orders.Include(o => o.User).Include(o => o.Goods).ToListAsync();
            Goods = await _context.Goods.ToListAsync();
        }

        // �������������� ������������
        public async Task<IActionResult> OnPostEditUserAsync(string login, string password, int role, int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.login = login;
                user.password = password;
                user.role = role;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������������� ������
        public async Task<IActionResult> OnPostEditOrderAsync(int count, int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.count = count;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������������� ������
        public async Task<IActionResult> OnPostEditGoodsAsync(string name, string description, bool inStock, int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods != null)
            {
                goods.name = name;
                goods.description = description;
                goods.in_stock = inStock;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������� ������������
        public async Task<IActionResult> OnPostDeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������� ������
        public async Task<IActionResult> OnPostDeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������� ������
        public async Task<IActionResult> OnPostDeleteGoodsAsync(int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods != null)
            {
                _context.Goods.Remove(goods);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }

}
