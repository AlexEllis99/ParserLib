using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace ParserLib.Settings
{
	class FindPropertiesSettings : IExecuter<string[]>
	{
		string propertyName;
		string propertyValue;
		string propertyNeeded;
		
		public FindPropertiesSettings(string propertyName, string propertyValue, string propertyNeeded)
		{
			this.propertyName = propertyName;
			this.propertyValue = propertyValue;
			this.propertyNeeded = propertyNeeded;
		}

		public string[] Go(IHtmlDocument d)
		{
			var elements  = d.QuerySelectorAll("div a td th span p img").Where(item => item.Attributes[propertyName].Value != null && item.Attributes[propertyName].Value == propertyValue);

			List<string> res = new List<string>();
			
			foreach(var element in elements)
			{
				if (element.Attributes[propertyNeeded] != null)
					res.Add(element.Attributes[propertyNeeded].Value);
			}
			return res.ToArray();
		}
	}
}
