using ID3DWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ID3DWeb.Controllers
{
	public class HomeController : _BaseController
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
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

		public IActionResult Index()
		{
			var currentProfile = new ProfileViewModel
			{
				PersonalInfo = new PersonalInfoSection { 
					Name = this.Current.FullName, 
					Graduated = this.Current.Nwo?.Graduated,
					Diploma = this.Current.Nwo?.Diploma,
					Status = this.Current.Nwo?.Status?.Text,
					MaxDevices = this.Current.MaxDevices 
				},

				ContactInfo = new ContactInfoSection { 
					Email = this.Current.Email, 
					Package = this.Current.Package?.Text 
				},

				//Preferences = new PreferencesSection { FavoriteColor = "Blue", ReceiveNewsletter = true }
			};

			return View(currentProfile);
		}
	}
}
