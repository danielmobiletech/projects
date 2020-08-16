using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using ChatApp.Models;


using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ChatApp.ViewComponents
{
    public class RoomViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public RoomViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            
            

           var userId= HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chat=_context.ChatUsers.Include(x=>x.Chat).Where(c=>c.UserId==userId && c.Chat.Type==ChatType.Room).Select(x=>x.Chat).ToList();
            
            return View(chat);
        }
    }

}
