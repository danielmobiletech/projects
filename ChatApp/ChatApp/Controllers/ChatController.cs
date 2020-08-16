using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chat;
        public ChatController(IHubContext<ChatHub> chat)
        {
            _chat = chat; 
        }
        // GET: /<controller>/
        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinRoom (string roomName,string connectionId)
        {
           await _chat.Groups.AddToGroupAsync(connectionId,roomName);
            return Ok();
        }
        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveRoom(string roomName, string connectionId)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }
        public async Task<IActionResult> SendMessage(string message,string roomName,[FromServices] AppDbContext ctx,int chatId)
        {
            var Message = new Message
            {
                Id = chatId,
                Text = message,
                TimeStamp = DateTime.Now,
                Name = User.Identity.Name


            };

            ctx.Messages.Add(Message);
            await ctx.SaveChangesAsync();

            await _chat.Clients.Group(roomName).SendAsync("RecieveMessage",Message);
            return Ok();
        }
    }
}
