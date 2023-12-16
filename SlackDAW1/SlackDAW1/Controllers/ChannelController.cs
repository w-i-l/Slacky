using Microsoft.AspNetCore.Mvc;

namespace SlackDAW1.Controllers
{
    public class ChannelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
