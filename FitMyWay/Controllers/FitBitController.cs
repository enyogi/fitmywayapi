using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FitMyWay.Controllers
{
	[RoutePrefix("api/fitbit")]
	public class FitBitController : ApiController
    {
		private const string OAuthClientID = "22CRLJ";
		private const string ClientSecret = "9cffe386f1bdd966cffa96592a7cac92";
		private const string CallBackURL = "https://www.fitmywayapp.com";
		private const string OAuthAuthorizationURI = "https://www.fitbit.com/oauth2/authorize";
		private const string AccessTokenRefreshURI = "https://api.fitbit.com/oauth2/token";

		[HttpPost]
		[Route("posttoken/user/{userId}/token/{accessToken}/refresh/{refreshToken}")]
		public void PostAccessTokens([FromBody]string accessToken, string refreshToken, int userId)
		{

		}
	}
}
