using Microsoft.AspNetCore.Mvc;
using SlackDAW1.Models;
using SlackDAW1.Data;
using ArticlesApp.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SlackDAW1.Controllers
{
    public class ChannelsController : Controller
    {

        private readonly ApplicationDbContext db;

        public ChannelsController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult New()
        {

            Channel channel = new Channel();

            var categories = CategoriesController.GetAllCategoriesToDisplayForForm(db);

			ViewBag.Categories = categories;

			return View(channel);
		}

        [HttpPost]
        public IActionResult New (Channel channel)
        {
            if(ModelState.IsValid)
            {
                db.Channels.Add(channel);
                db.SaveChanges();
                TempData["message"] = "Channel was added";
                return RedirectToAction("Index");
            }
            else
            {
				var categories = CategoriesController.GetAllCategoriesToDisplayForForm(db);

				ViewBag.Categories = categories;

				return View(channel);
            }
        }

        public IActionResult Edit(int id)
        {
            Channel channel = db.Channels.Include("Category")
                                .Where(chan => chan.ChannelID == id)
                                .First();

            var categories = CategoriesController.GetAllCategoriesToDisplayForForm(db);
            ViewBag.Categories = categories;

            return View(channel);
        }

        [HttpPost]
        public IActionResult Edit(int id, Channel requestChannel)
        {

            Channel channel = db.Channels.Find(id);

			if(ModelState.IsValid && channel != null)
            {

				channel.ChannelName = requestChannel.ChannelName;
                channel.ChannelDescription = requestChannel.ChannelDescription;
                channel.CategoryID = requestChannel.CategoryID;
                db.SaveChanges();
                TempData["message"] = "Channel was edited";
                return RedirectToAction("Index");
			}
			else
            {
				var categories = CategoriesController.GetAllCategoriesToDisplayForForm(db);
				ViewBag.Categories = categories;

				return View(channel);
			}
		}
        
        public IActionResult Delete(int id)
        {
			Channel channel = db.Channels.Find(id);
			db.Channels.Remove(channel);
			db.SaveChanges();
            TempData["message"] = "Channel was deleted";
			return RedirectToAction("Index");
		}

        public IActionResult Show(int id)
        {
			Channel channel = db.Channels.Include("Category")
								.Where(chan => chan.ChannelID == id)
								.First();

			return View(channel);
		}

        public IActionResult Index()
        {

            var channels = db.Channels.Include("Category");

            ViewBag.Channels = channels;

            return View();
        }

		[NonAction]
		public static IEnumerable<SelectListItem> GetAllChannelsToDisplayForForm(ApplicationDbContext db)
		{
			var selectList = new List<SelectListItem>();

            var channels = from chan in db.Channels
                           select chan;

            foreach (var channel in channels)
			{
				selectList.Add(new SelectListItem
				{
					Value = channel.ChannelID.ToString(),
					Text = channel.ChannelName.ToString()
				});
			}
			return selectList;
		}
	}
}
