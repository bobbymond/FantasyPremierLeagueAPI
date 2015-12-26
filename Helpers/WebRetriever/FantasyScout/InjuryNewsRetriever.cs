#region license
// Copyright (c) 2015 Mark Hammond
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
using System.Globalization;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.FantasyScout
{
    public class InjuryNewRetriever
    {
        private const   string  _INJURYNEWS_PAGE = "http://www.fantasyfootballscout.co.uk/fantasy-football-injuries/";
        private         ILogger _logger;

        public InjuryNewRetriever(ILogger logger)
        {
            _logger = logger;
        }

        /// <returns>[Club => [Player Name => Player Return Date]]</returns>
        public Dictionary<IClub, Dictionary<string, DateTime>> GetInjuryNews()
        {
            var requester = new WebPageRequester(_logger);

            var url = _INJURYNEWS_PAGE;

            CookieContainer cookies = null;
            var response = requester.Get(url, ref cookies);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

            var contentElement = htmlDoc.GetElementbyId("content");
            var injuriesTableBody = contentElement.SelectSingleNode("//table[@class='ffs-ib ffs-ib-full-content ffs-ib-sort']/tbody");

            var items = injuriesTableBody.Elements("tr"); // force evaluation

            var result = new Dictionary<IClub, Dictionary<string, DateTime>>();

            foreach (var item in items)
            {
                var clubName = item.SelectSingleNode(".//td[@class='team']").GetAttributeValue("title", null);
                if (clubName == null)
                    continue;

                var allCols = item.SelectNodes("td");
                if (allCols.Count < 4)
                    continue;

                var playerName = allCols[0].InnerText.Trim();
                if (playerName.Contains('('))
                    playerName = playerName.Split('(')[0].Trim();

                var returnDateStr = allCols[3].InnerText.Trim();
                DateTime returnDate;
                if (!DateTime.TryParseExact(returnDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out returnDate))
                    continue;


                var club = Clubs.GetClubFromName(clubName);

                Dictionary<string, DateTime> playerDict;
                if (!result.TryGetValue(club, out playerDict))
                {
                    playerDict = new Dictionary<string, DateTime>();
                    result.Add(club, playerDict);
                }

                if (playerDict.ContainsKey(playerName))
                    _logger.WriteErrorMessage(string.Format("Injury news already container entry for {0}({1})", playerName, clubName));
                else
                    playerDict.Add(playerName, returnDate);

                var i = 1;
            }

            return result;

        }

    }
}
