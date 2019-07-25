using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib.Settings
{
	interface IExecuter<T> where T : class
	{
		T Go(IHtmlDocument d);
	}
}
