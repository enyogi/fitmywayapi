using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Athenahealth;

namespace FitMyWay.Controllers
{
	[RoutePrefix("api/athena")]
	public class AthenaController : ApiController
    {
		private const string APIKey = "r5838xews2mb4bwzw3d6ds2q";
		private const string Secret = "yTgYN73HZSzGAvU";

		[HttpGet]
		[Route("publish")]
		public HttpResponseMessage Publish()
		{
			string key = APIKey;
			string secret = Secret;
			string version = "preview1";
			string practiceid = "1959540";

			APIConnection api = new APIConnection(version, key, secret, practiceid);

			// preview1 /:practiceid / appointments /:appointmentid / notes
			var path = "/preview1/"+ practiceid + "/appointments/" + "982123" + "/notes";
			//application/x-www-form-urlencoded

			var payload = new Dictionary<string, string>();
			payload.Add("notetext", "Calories Burned In Last 30 Days: 42102. Distance covered: 12 miles. Days Active: 26, Floors Climbed: 13, Points Earned: 86");

			try
			{
				api.POST(path, payload);
				return new HttpResponseMessage(HttpStatusCode.OK);
			}
			catch(Exception ex)
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
		}


	}
}
