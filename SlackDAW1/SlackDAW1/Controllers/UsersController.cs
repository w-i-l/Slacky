using Microsoft.AspNetCore.Authorization;
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
		public IActionResult Invite(int? id, string userId)
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

		[Authorize(Roles = "Admin, Moderator, User")]
		[HttpPost]
		public IActionResult RemoveUserFromChannel(string userId, int channelId)
		{
			var me = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

			if (me == null)
			{
				return RedirectToAction("Index", "Home");
			}

			// Check if the logged-in user is an administrator for the channel
			var myRoleInChannel = db.UserChannels
				.FirstOrDefault(uc => uc.UserID == me.Id && uc.ChannelID == channelId && uc.IsModerator);

			if (myRoleInChannel == null)
			{
				// TODO
				// If the user is not an administrator, redirect to home or handle it as needed
				return RedirectToAction("Index", "Home");
			}

			var userChannel = db.UserChannels
				.SingleOrDefault(uc => uc.UserID == userId && uc.ChannelID == channelId);

			if (userChannel != null)
			{
				db.UserChannels.Remove(userChannel);
				db.SaveChanges();
			}
			else
			{
				// Handle the case where the userChannel is not found (sequence is empty)
				// You might want to log this or return an appropriate response.
			}

			return RedirectToAction("Show", "Channels", new { id = channelId });
		}

		[Authorize(Roles = "Admin, Moderator, User")]
		[HttpPost]
		public IActionResult PromoteUserOnChannel(string userId, int channelId)
		{
			var me = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

			if (me == null)
			{
				return RedirectToAction("Index", "Home");
			}

			// Check if the logged-in user is an administrator for the channel
			var myRoleInChannel = db.UserChannels
				.FirstOrDefault(uc => uc.UserID == me.Id && uc.ChannelID == channelId && uc.IsModerator);

			if (myRoleInChannel == null)
			{
				// TODO
				// If the user is not an administrator, redirect to home or handle it as needed
				return RedirectToAction("Index", "Home");
			}

			var userChannel = db.UserChannels
				.SingleOrDefault(uc => uc.UserID == userId && uc.ChannelID == channelId);

			if (userChannel != null)
			{
				userChannel.IsModerator = true;
				db.SaveChanges();
			}
			else
			{
				// Handle the case where the userChannel is not found (sequence is empty)
				// You might want to log this or return an appropriate response.
			}

			return RedirectToAction("Show", "Channels", new { id = channelId });
		}

		[Authorize(Roles = "Admin, Moderator, User")]
		[HttpPost]
		public IActionResult DemoteUserOnChannel(string userId, int channelId)
		{
			var me = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

			if (me == null)
			{
				return RedirectToAction("Index", "Home");
			}

			// Check if the logged-in user is an administrator for the channel
			var myRoleInChannel = db.UserChannels
				.FirstOrDefault(uc => uc.UserID == me.Id && uc.ChannelID == channelId && uc.IsModerator);

			if (myRoleInChannel == null)
			{
				// TODO
				// If the user is not an administrator, redirect to home or handle it as needed
				return RedirectToAction("Index", "Home");
			}

			var userChannel = db.UserChannels
				.SingleOrDefault(uc => uc.UserID == userId && uc.ChannelID == channelId);

			if (userChannel != null)
			{
				userChannel.IsModerator = false;
				db.SaveChanges();
			}
			else
			{
				// Handle the case where the userChannel is not found (sequence is empty)
				// You might want to log this or return an appropriate response.
			}

			return RedirectToAction("Show", "Channels", new { id = channelId });
		}

		[Authorize(Roles = "Admin, Moderator, User")]
		[HttpPost]
		public IActionResult LeaveChannel(int channelId)
		{
			var me = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

			if (me == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var userChannel = db.UserChannels
				.SingleOrDefault(uc => uc.UserID == me.Id && uc.ChannelID == channelId);

			if (userChannel != null)
			{
				db.UserChannels.Remove(userChannel);
				db.SaveChanges();
			}
			else
			{
				// Handle the case where the userChannel is not found (sequence is empty)
				// You might want to log this or return an appropriate response.
			}

			return RedirectToAction("Index", "Channels");
		}



		public IActionResult Index()
		{
			return View();
		}
	}
}
