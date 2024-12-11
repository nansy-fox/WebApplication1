using NuGet.Packaging.Signing;

namespace WebApplication1.Models
{
    public class Order
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int goods_id { get; set; }
        public int count { get; set; } 
        // Навигационные свойства
        public Users User { get; set; }
        public Good Goods { get; set; }
    }
}
