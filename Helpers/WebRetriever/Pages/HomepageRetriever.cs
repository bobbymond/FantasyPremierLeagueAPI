#region license
// Copyright (c) 2013 Mark Hammond
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using FantasyPremierLeagueApi.Helpers.Logger;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.Page
{
    public class HomepageRetriever
    {
        private const   string  _URL_HOMEPAGE = "http://fantasy.premierleague.com";
        private         ILogger _logger;

        public HomepageRetriever(ILogger logger)
        {
            _logger = logger;
        }

        public int GetCurrentGameweek(CookieContainer cookies)
        {
            var requester = new WebPageRequester(_logger);

            var response = requester.Get(_URL_HOMEPAGE, ref cookies);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            var ismElement = htmlDoc.GetElementbyId("ism");
            var ismEventInfoPrimary = ismElement.SelectSingleNode("//div[@class='ism-event-info']");
            if (ismEventInfoPrimary == null) // hack for now - see if this still works come gameweek 2 :-)
                return 1;
            var ismMegaLargeParagraph = ismEventInfoPrimary.SelectSingleNode("//h3[@class='ism-event-info__sub-heading ism-pl-font']");
            var gameweekNum = ismMegaLargeParagraph.InnerText.Replace("Gameweek", "").Trim();

            return int.Parse(gameweekNum);
        }

    }
}
