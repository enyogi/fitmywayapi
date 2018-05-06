using FitMyWay.Library;
using System;
using System.Web.Http;
using Fitbit.Api.Portable;
using System.Configuration;
using Fitbit.Api.Portable.OAuth2;

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

		[HttpGet]
		[Route("posttoken/code={code}")]
		public object GetAuthCode(string code)
		{
			var authorization = "Basic Y2xpZW50X2lkOjljZmZlMzg2ZjFiZGQ5NjZjZmZhOTY1OTJhN2NhYzky";
			var contenttype = "application/x-www-form-urlencoded";
			var redirectURI = "http://localhost:50195/api/fitbit/callback";
			//var redirectURI = "https://testfit.azurewebsites.net/api/fitbit/callback";
			var queryParams = "client_id=" + OAuthClientID + "&grant_type=authorization_code&redirect_uri=" + redirectURI + "&code=" + code;



			var appCredentials = new FitbitAppCredentials()
			{
				ClientId = ConfigurationManager.AppSettings["FitbitClientId"],
				ClientSecret = ConfigurationManager.AppSettings["FitbitClientSecret"]
			};
			//make sure you've set these up in Web.Config under <appSettings>:

			//Session["AppCredentials"] = appCredentials;

			//Provide the App Credentials. You get those by registering your app at dev.fitbit.com
			//Configure Fitbit authenticaiton request to perform a callback to this constructor's Callback method
			var authenticator = new OAuth2Helper(appCredentials, redirectURI);
			string[] scopes = new string[] { "profile","activity","heartrate","location","nutrition","settings","sleep","social","weight" };

			string authUrl = authenticator.GenerateAuthUrl(scopes, null);

			return Redirect(authUrl);

			/*
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(AccessTokenRefreshURI);

			client.DefaultRequestHeaders.Accept
				.Add(new MediaTypeWithQualityHeaderValue(contenttype));//ACCEPT header
			client.DefaultRequestHeaders.Add("Authorization", authorization);

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, queryParams);
			
		    client.SendAsync(request)
				  .ContinueWith(responseTask =>
				  {
					  var response = responseTask.Result;
					  return response;
				  });

			return null;
			*/
		}

		[HttpGet]
		[Route("callback")]
		public void GetAccessTokens()
		{
			Token token = new Token();
			var dbConnector = new DBConnector();

			dbConnector.SaveFitBitAccessTokens(token.access_token, token.refresh_token);
		}
	}
}
