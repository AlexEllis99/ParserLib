using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib
{
	class HtmlLoader
	{
		HttpClient client = new HttpClient();

		public async Task<string> GetHtmlAsync(string url)
		{
			string res = null;
			HttpResponseMessage response = await client.GetAsync(url);
			if (response != null && response.StatusCode == HttpStatusCode.OK)
				res = await response.Content.ReadAsStringAsync();
			return res;
		}
	}
}
