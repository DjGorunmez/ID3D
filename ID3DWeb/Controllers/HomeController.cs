using ID3DWeb.Models;
using ID3DWebLib;
using ID3DWebLib.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace ID3DWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public Profile Current
		{
			get 
			{
				return JsonSerializer.Deserialize<Profile>(HttpContext.Session.GetString("CurrentProfile"));
			}
			set 
			{ 
				HttpContext.Session.SetString("CurrentProfile", JsonSerializer.Serialize(value));
			}
		}

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
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

				if(this.Current == null)
				{
					ViewBag.Message = "Invalid username or password.";
					return View();
				}

				// Simulate successful login
				ViewBag.Message = "Login successful!";
			}
			else
			{
				ViewBag.Message = "Please enter username and password.";
			}
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
