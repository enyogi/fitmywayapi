using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitMyWay.Controllers
{
	public class HomeController : Controller
	{
		private const string OAuthClientID = "22CRLJ";
		private const string ClientSecret = "9cffe386f1bdd966cffa96592a7cac92";
		private const string CallBackURL = "https://testfit.azurewebsites.net/api/fitbit/posttoken/";
		private const string OAuthAuthorizationURI = "https://www.fitbit.com/oauth2/authorize";
		private const string AccessTokenRefreshURI = "https://api.fitbit.com/oauth2/token";

		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			

			return View();
		}
	}
}
