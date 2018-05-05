using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FitMyWay.Controllers
{
	[RoutePrefix("api/activity")]
	public class ActivitiesController : ApiController
    {
		//GetUserActivitySuggestions
		[HttpGet]
		[Route("user/{userId}/all")]
		public IEnumerable<Library.Activity> GetUserActivitySuggestions(int userId)
		{
			var dbConnector = new Library.DBConnector();

			return dbConnector.GetUserActivitySuggestions(userId).Result;
		}
	}
}
