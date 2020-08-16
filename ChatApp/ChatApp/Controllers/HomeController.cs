using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Private()
        {

            var chats = _context.Chats.Include(x => x.Users).ThenInclude(x=>x.User)
                .Where(x => x.Type == ChatType.Private && x.Users.Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList();
           return View(chats);
        }


        public IActionResult Index()
        {
            var chats = _context.Chats.Include(x=>x.Users).Where(x=> !x.Users.Any(y=>y.UserId== User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList();
            return View(chats);
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat=_context.Chats
                .Include(x=>x.Messages)

                .FirstOrDefault(x => x.Id == id);
            return View(chat);


        }

        public async Task<IActionResult> CreatePrivateRoom(string userid)
        {
            var chat = new Chat
            {
                Type = ChatType.Private
            };

            chat.Users.Add(new ChatUser {

                UserId=userid

            });

            chat.Users.Add(new ChatUser {

                UserId= User.FindFirst(ClaimTypes.NameIdentifier).Value
            });

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = userid });

        }


        public IActionResult Find()
        {

            var user = _context.Users.Where(x => x.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList();
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var Message = new Message
            {
                Id = chatId,
                Text = message,
                TimeStamp = DateTime.Now,
                Name = User.Identity.Name


            };

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Chat", new { id = chatId });


        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room


            };

            chat.Users.Add(new ChatUser
            {
                Role=UserRole.Admin,
                UserId=User.FindFirst(ClaimTypes.NameIdentifier).Value

            });
            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> JoinRoom(int id)
        {
            var chat= new ChatUser
            {
                ChatId=id,
                Role = UserRole.Member,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value

            };
            _context.ChatUsers.Add(chat);

            await _context.SaveChangesAsync();
            return RedirectToAction("Chat","Home",new { id=id});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
