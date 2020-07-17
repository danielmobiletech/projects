using System;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;

namespace Shop.Application.OrdersAdmin
{
    public class UpdateOrder
    {
        ApplicationDbContext _context;
        public UpdateOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoAsync(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            order.Status += 1;
            return await _context.SaveChangesAsync() > 0;

        }

    }
}
