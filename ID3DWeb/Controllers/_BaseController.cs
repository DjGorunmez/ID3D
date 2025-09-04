using ID3DWeb.Models;
using ID3DWebLib;
using ID3DWebLib.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ID3DWeb.Controllers
{
	public class _BaseController : Controller
	{
		public Profile Current
		{
			get
			{
				var profile = HttpContext.Session.GetString("CurrentProfile");

				if(!string.IsNullOrEmpty(profile))
					return JsonSerializer.Deserialize<Profile>(profile);

				return null;
			}
			set
			{
				HttpContext.Session.SetString("CurrentProfile", JsonSerializer.Serialize(value));
			}
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (Current == null && context.ActionDescriptor.RouteValues["action"] != "Login")
			{
				context.Result = RedirectToAction("Login", "Home");
				return;
			}

			base.OnActionExecuting(context);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View(new LoginViewModel());
		}

		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			// Placeholder for login logic
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				ProfileManager profileManager = new ProfileManager();
				this.Current = profileManager.Login(username, password);

				if (this.Current == null)
				{
					ViewBag.Message = "Invalid username or password.";
					return View("Login");
				}

				// Simulate successful login
				ViewBag.Message = "Login successful!";
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Message = "Please enter username and password.";
			}

			return View("Login");
		}
	}
}
