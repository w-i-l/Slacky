using Microsoft.AspNetCore.Mvc;

namespace SlackDAW1.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
