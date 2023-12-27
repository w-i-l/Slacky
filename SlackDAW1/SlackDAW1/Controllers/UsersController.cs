using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SlackDAW1.Data;
using SlackDAW1.Models;

namespace SlackDAW1.Controllers
{
	public class UsersController : Controller
	{

		private readonly ApplicationDbContext db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UsersController(
			ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager
		)
		{
			db = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Invite(int? id)
		{
			var allUsers = db.Users.ToList();
			var usersInChannel = db.UserChannels.Where(uc => uc.ChannelID == id).Select(uc => uc.User).ToList();

			var usersNotInChannel = allUsers.Except(usersInChannel).ToList();

			ViewBag.UsersNotInChannel = usersNotInChannel;
			ViewBag.ChannelID = id;

			return View();
		}

		[HttpPost]
		public IActionResult Invite(int? id,  string userId)
		{
			var userChannel = new UserChannel
			{
				UserID = userId,
				ChannelID = (int)id,
				IsModerator = false
			};

			db.UserChannels.Add(userChannel);
			db.SaveChanges();

			var allUsers = db.Users.ToList();
			var usersInChannel = db.UserChannels.Where(uc => uc.ChannelID == id).Select(uc => uc.User).ToList();

			var usersNotInChannel = allUsers.Except(usersInChannel).ToList();

			ViewBag.UsersNotInChannel = usersNotInChannel;
			ViewBag.ChannelID = id;

			return View();
		}


		public IActionResult Index()
		{
			return View();
		}
	}
}
