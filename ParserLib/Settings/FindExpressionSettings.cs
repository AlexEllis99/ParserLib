using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace ParserLib.Settings
{
	class FindExpressionSettings : IExecuter<string>
	{
		Regex expression;

		public FindExpressionSettings(string expression) => this.expression = new Regex(expression);

		public string Go(IHtmlDocument d)
		{
			string res = null;
			if (expression.IsMatch(d.TextContent))
				res = d.Url;
			return res;
		}
	}
}
