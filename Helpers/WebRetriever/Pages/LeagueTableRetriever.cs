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
using FantasyPremierLeagueApi.Helpers.Logger;
using System.Net;
using HtmlAgilityPack;
using FantasyPremierLeagueApi.Model;
using FantasyPremierLeagueApi.Model.Club;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.Pages
{
    public class LeagueTableRetriever
    {
        private const   string  _LEAGUETABLE_HOMEPAGE   = "http://www.premierleague.com/en-gb/matchday/league-table.html?tableView=HOME_VS_AWAY";
        private const   string  _PARAM_SEASON           = "season";
        private         ILogger _logger;

        public LeagueTableRetriever(ILogger logger)
        {
            _logger = logger;
        }

        /// <param name="season">A string in the form 2012-2013</param>
        public IEnumerable<ClubSeasonPerformance> GetLeagueTable(CookieContainer cookies, string season)
        {
            var requester = new WebPageRequester(_logger);

            var url = string.Format("{0}&{1}={2}", _LEAGUETABLE_HOMEPAGE, _PARAM_SEASON, season);
            var response = requester.Get(url, ref cookies);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            var bodyElement = htmlDoc.GetElementbyId("body");
            var leagueTableBody = bodyElement.SelectSingleNode("//table[@class='leagueTable home-away']//tbody");

            var rows = leagueTableBody.Elements("tr");

            return rows.Select(row => ParseLeagueTableRow(row));

        }

        #region Private Methods
        
        private ClubSeasonPerformance ParseLeagueTableRow(HtmlNode tableRow)
        {
            var cells = tableRow.Elements("td").ToArray();

            var name = cells[1].InnerText;

            return new ClubSeasonPerformance
            {
                Club = Clubs.GetClubFromName(name),
                HomePlayed = byte.Parse(cells[2].InnerText),
                HomeWon = byte.Parse(cells[3].InnerText),
                HomeDrawn = byte.Parse(cells[4].InnerText),
                HomeLost = byte.Parse(cells[5].InnerText),
                HomeGoalsFor = byte.Parse(cells[6].InnerText),
                HomeGoalsAgainst = byte.Parse(cells[7].InnerText),
                AwayPlayed = byte.Parse(cells[10].InnerText),
                AwayWon = byte.Parse(cells[11].InnerText),
                AwayDrawn = byte.Parse(cells[12].InnerText),
                AwayLost = byte.Parse(cells[13].InnerText),
                AwayGoalsFor = byte.Parse(cells[14].InnerText),
                AwayGoalsAgainst = byte.Parse(cells[15].InnerText)

            };
               
        }

        #endregion

    }
}
