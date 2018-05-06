using FitMyWay.Library;
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
		private const string CallBackURL = "https://testfit.azurewebsites.net/api/fitbit/posttoken/";
		private const string OAuthAuthorizationURI = "https://www.fitbit.com/oauth2/authorize";
		private const string AccessTokenRefreshURI = "https://api.fitbit.com/oauth2/token";

		[HttpPost]
		[Route("posttoken/?code={code}")]
		public void GetAuthCode(string code)
		{
			var authorization = "Basic Y2xpZW50X2lkOjljZmZlMzg2ZjFiZGQ5NjZjZmZhOTY1OTJhN2NhYzky";
			var contenttype = "application/x-www-form-urlencoded";
			var redirectURI = "https://testfit.azurewebsites.net/api/fitbit/auth/";
			var queryParams = "client_id=" + OAuthClientID + "&grant_type=authorization_code&redirect_uri=" + redirectURI + "&code=" + code;

			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(AccessTokenRefreshURI + "?" + queryParams);

			client.DefaultRequestHeaders.Add("Content-Type", contenttype);
			client.DefaultRequestHeaders.Add("Authorization", authorization);

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,"");
			
			client.SendAsync(request)
				  .ContinueWith(responseTask =>
				  {
					  var response = responseTask.Result;
				  });
		}

		[HttpPost]
		[Route("auth/")]
		public void GetAccessTokens([FromBody] Token token)
		{
			var dbConnector = new DBConnector();

			dbConnector.SaveFitBitAccessTokens(token.access_token, token.refresh_token);
		}
	}
}
