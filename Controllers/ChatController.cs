using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Example.Models;
using Example.Data;

namespace Example.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ChatController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var users = (from user in _context.Users  
                        select new  
                        {  
                            UserId = user.Id,                                        
                            FirstName = user.FirstName,  
                            LastName = user.LastName,
                            Picture = user.Picture,
                            Email = user.Email,
                            City = user.City,
                            State = user.State
                        }).ToList()
                        .Select(u => new ApplicationUser()  
                        {  
                            Id = u.UserId,  
                            FirstName = u.FirstName, 
                            LastName = u.LastName, 
                            Picture = u.Picture,
                            Email = u.Email,
                            City = u.City,
                            State = u.State
                        });  
                        
            users = users.OrderBy(u => u.Email);
            return View(users.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Conversation(string id)
        {    
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            var messages = (from message in _context.Messages
                            select new   
                            { 
                                MessageId = message.MessageId,
                                SenderId = message.SenderId,
                                ReceiverId = message.ReceiverId,
                                Message = message.Message,
                                CreateDate = message.CreateDate
                            }).Select(m => new MessageModel()  
                            {  
                                MessageId = m.MessageId,
                                SenderId = m.SenderId,
                                ReceiverId = m.ReceiverId,
                                Message = m.Message,
                                CreateDate = m.CreateDate
                            })
                            .Where(c => (c.ReceiverId == id && c.SenderId == userId.ToString()) || 
                                            (c.ReceiverId == userId.ToString() && c.SenderId == id))
                            .OrderBy(c => c.CreateDate);

            return View(messages.ToList());
        }

        public IActionResult SendMessage(string id, string content, MessageModel message)
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            message.ReceiverId = id;
            message.Message = content; // Get a warning here. Should probably change Message to Content as well.
            message.SenderId = userId;
            message.Status = 0; // Not really need. Kind of a false start.
            message.CreateDate = DateTime.Now;


            _context.Messages.Add(message);
            _context.SaveChangesAsync();

            return RedirectToAction("Conversation", new {id = id});
        }
    }
}
