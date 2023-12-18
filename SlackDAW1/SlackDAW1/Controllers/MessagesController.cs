using Microsoft.AspNetCore.Mvc;
using SlackDAW1.Models;
using SlackDAW1.Data;
using Microsoft.EntityFrameworkCore;

//chiar nu inteleg de ce nu accepta messages
//nvm acum merge

namespace SlackDAW1.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext db;

        public MessagesController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var messages = db.Messages;
            return View(messages);
        }

       
        public IActionResult New()
        {
            return View();
        }

      
        [HttpPost]
        public IActionResult New(Message message)
        {
            message.Timestamp = DateTime.Now;
            message.SenderID = 1;
            message.ChannelID = 1;
            if (ModelState.IsValid)
            {   
                db.Messages.Add(message);
                db.SaveChanges();
                TempData["message"] = "Message was added";
                return RedirectToAction("Index");
            }
            return View(message);
        }

    
        public IActionResult Edit(int id)
        {
            var message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }


        [HttpPost]
        public IActionResult Edit(int id, Message requestMessage)
        {
            var message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                message.Body = requestMessage.Body;
               
                db.SaveChanges();
                TempData["message"] = "Message was edited";
                return RedirectToAction("Index");
            }
            return View(message);
        }

       
        public IActionResult Delete(int id)
        {
            var message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            db.SaveChanges();
            TempData["message"] = "Message was deleted";
            return RedirectToAction("Index");
        }

       
        public IActionResult Show(int id)
        {
            var message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }
    }
}