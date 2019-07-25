using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace ParserLib.Settings
{
	class GetInnerUrlsSettings : IExecuter<string>
	{
		public string Go(IHtmlDocument d) => d.Url;
	}
}
