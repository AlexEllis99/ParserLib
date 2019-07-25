using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ParserLib.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib
{ 
	public class Parser
	{
		readonly string startUrl;
		readonly string baseUrl;

		public string[] ExcludedDistricts { get; set; }
		HtmlLoader loader = new HtmlLoader();
		HtmlParser parser = new HtmlParser();


		public Parser(string startUrl, string baseUrl)
		{
			this.startUrl = startUrl;
			this.baseUrl = baseUrl;
		}

		async Task<T[]> SerfAsync<T>(IExecuter<T> exec) where T : class
		{
			List<T> resList = new List<T>();

			List<string> GetLinks (IHtmlDocument doc)
			{
				List<string> links = new List<string>();
				var allLinks = doc.QuerySelectorAll("a");
				foreach (var link in allLinks)
				{
					string l = link.Attributes["href"].Value;
					if (l.StartsWith(@"/"))
					{
						foreach (string s in ExcludedDistricts)
						{
							if (l.Contains(s)) goto next;
						}
						links.Add(l);
					}
					next:;
				}
				return links;
			}


			List<string> urls = new List<string>() { startUrl };
			List<string> urlsToGo = new List<string>() { startUrl };
			List<string> urlsFound = new List<string>();

			while (urlsToGo.Count > 0)
			{
				string page = await loader.GetHtmlAsync(urlsToGo[0]);

				IHtmlDocument doc = await parser.ParseDocumentAsync(page);
				T middleResult = exec.Go(doc);
				if (middleResult != null) resList.Add(middleResult);

				urlsFound = GetLinks(doc);
				foreach (string urlFound in urlsFound)
				{
					if (!(urls.Contains(urlFound)))
					{
						urls.Add(urlFound);
						urlsToGo.Add(urlFound);
					}
				}
				urlsToGo.RemoveAt(0);
			}

			return resList.ToArray();
		}

		public async Task<string[]> FindExpressionAsync(string expression) => await SerfAsync(new FindExpressionSettings(expression));

		public async Task<string[]> GetInnerUrlsAsync() => await SerfAsync(new GetInnerUrlsSettings());

		public async Task<string[][]> FindPropertiesAsync(string propertyName, string propertyValue, string propertyNeeded) => await SerfAsync(new FindPropertiesSettings(propertyName, propertyValue, propertyNeeded));
	}
}
