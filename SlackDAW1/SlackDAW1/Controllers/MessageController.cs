using Microsoft.AspNetCore.Mvc;

namespace SlackDAW1.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
