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
using System.Net;
using System.Text;
using Newtonsoft;
using FantasyPremierLeagueApi.Helpers.Logger;
using FantasyPremierLeagueApi.Model.Player;
using Newtonsoft.Json;
using HtmlAgilityPack;
using FantasyPremierLeagueApi.Model.Club;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.FantasyScout
{
    public class TeamNewsRetriever
    {
        private const   string  _TEAMNEWS_PAGE = "http://www.fantasyfootballscout.co.uk/team-news/";
        private         ILogger _logger;

        public TeamNewsRetriever(ILogger logger)
        {
            _logger = logger;
        }

        public Dictionary<IClub,IEnumerable<string>> GetTeamNews()
        {
            var requester = new WebPageRequester(_logger);

            var url = _TEAMNEWS_PAGE;

            CookieContainer cookies = null;
            var response = requester.Get(url, ref cookies);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            var contentElement = htmlDoc.GetElementbyId("content");
            var leagueTableBody = contentElement.SelectSingleNode("//div[@class='ffs-team-news']/ol[@class='news']");

            var items = leagueTableBody.Elements("li");

            var result = new Dictionary<IClub, IEnumerable<string>>();
            foreach(var item in items)
            {
                var clubName = item.SelectSingleNode("h2").InnerText;
                var players = ParseExpectedTeam(item);

                var club = Clubs.GetClubFromName(clubName);

                result.Add(club, players);
            }

            return result;

        }

        private IEnumerable<string> ParseExpectedTeam(HtmlNode item)
        {
            var playerNodes = item.SelectNodes("//li/span[@class='player-name']");

            return playerNodes.Select(node => node.InnerText);
        }
    }
}
