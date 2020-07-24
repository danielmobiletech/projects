using System;
using System.Linq;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.ViewComponents
{
    public class RoomViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public RoomViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public ViewComponent Invoke()
        {
            var chat=_context.Chats.ToList();
            View(chat);
        }
    }

}
