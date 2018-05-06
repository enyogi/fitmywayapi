using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitMyWay
{
	public class Token
	{
		public string access_token { get; set; }
		public string refresh_token { get; set; }
		public string token_type { get; set; }
		public string user_id { get; set; }
	}
}