using Microsoft.AspNetCore.Mvc;
using SlackDAW1.Models;
using SlackDAW1.Data;
using SlackDAW1.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SlackDAW1.Controllers
{
    public class ChannelsController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ChannelsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

                db.UserChannels.Add(new UserChannel
                {
                    UserID = _userManager.GetUserId(User),
                    ChannelID = channel.ChannelID,
                    IsModerator = true
                });
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
            Channel channel = db.Channels
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ChannelID == id);

            if (channel == null)
            {
                // Handle the case where the channel is not found
                return NotFound();
            }

            // Get the users associated with the channel along with moderator status
            var usersWithModeratorStatus = db.UserChannels
                .Where(uc => uc.ChannelID == id)
                .Select(uc => new { User = uc.User, IsModerator = uc.IsModerator })
                .ToList();

            var messages = db.Messages
                .Where(m => m.ChannelID == id)
                .OrderBy(m => m.Timestamp)
                .Select(m => new {Body = m.Body, Timestamp = m.Timestamp, Sender = m.Sender, MessageID = m.MessageID})
                .ToList();

            var isUserModerator = db.UserChannels
                .Where(uc => uc.ChannelID == id && uc.UserID == _userManager.GetUserId(User))
                .Select(uc => uc.IsModerator)
                .First();

            ViewBag.IsUserModerator = isUserModerator == true;
            ViewBag.UsersWithModeratorStatus = usersWithModeratorStatus;
            ViewBag.Messages = messages;

            return View(channel);
        }




        public IActionResult Index()
        {

            var user = _userManager.GetUserId(User);

            /*i want to get the channels in whicj the user is with the category*/
            var channels = db.UserChannels
                .Where(uc => uc.UserID == user)
                .Include(c => c.Channel.Category)
                .Select(c => c.Channel);
                
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
