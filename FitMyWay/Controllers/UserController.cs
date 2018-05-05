using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FitMyWay.Controllers
{
	[RoutePrefix("api/user")]
	public class UserController : ApiController
    {
		[HttpGet]
		[Route("{userId}/activity/")]
		public IEnumerable<Library.Activity> GetUserActivitySuggestions(int userId)
		{
			var dbConnector = new Library.DBConnector();

			return dbConnector.GetUserActivitySuggestions(userId).Result;
		}
	}
}
