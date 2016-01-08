using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FantasyPremierLeagueApi.Helpers.Logger;
using System.Net;
using HtmlAgilityPack;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.Pages
{
    public class TransferPageRetriever
    {
        private const   string  _TRANSFERS_PAGE   = "http://fantasy.premierleague.com/transfers/";
        private         ILogger _logger;
        private         string  _transfersPageHtmlString;

        public TransferPageRetriever(ILogger logger, CookieContainer cookies)
        {
            _logger = logger;

            var requester = new WebPageRequester(_logger);
            _transfersPageHtmlString = requester.Get(_TRANSFERS_PAGE, ref cookies);
        }

        /// <returns>Dictionary mapping player id to transfer value in 100,000's</returns>
        public Dictionary<int,int> GetMyTeamTransferValues()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(_transfersPageHtmlString);
            
            //var body = htmlDoc.GetElementbyId("ismTeamDisplayGraphical");
            //var ismPitch = body.SelectSingleNode("//div[@class='ismPitch']");
            //var players = ismPitch.SelectNodes("//div[@class='ismPlayerContainer']");

            var result = new Dictionary<int, int>();

            for (int i = 0; i < 15; i++)
            {
                string playerIdStr = htmlDoc.GetElementbyId(string.Format("id_pick_formset-{0}-element", i)).GetAttributeValue("value", "");
                int playerId = int.Parse(playerIdStr);

                string salePriceStr = htmlDoc.GetElementbyId(string.Format("id_pick_formset-{0}-selling_price", i)).GetAttributeValue("value", "");
                int salePrice = int.Parse(salePriceStr);

                result.Add(playerId, salePrice);
            }

            /*foreach (var player in players)
            {
                string playerIdStr = player.SelectSingleNode("//a[@class='ismViewProfile']").GetAttributeValue("href",""); // string in form "#<id>"
                int playerId = int.Parse(playerIdStr.TrimStart('#'));

                string salePriceStr = player.SelectSingleNode("//span[@class='ismPitchStat']").InnerText; // string in form "£<sell price>"
                decimal salePrice = decimal.Parse(salePriceStr.TrimStart('£'));

                result.Add(playerId, salePrice);
            }*/

            return result;
        }

        /// <returns>The amount the logged on user has in the bank</returns>
        public decimal GetRemainingBudget()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(_transfersPageHtmlString);
            var ismToSpendElement = htmlDoc.GetElementbyId("ismToSpend");

            var remainingBudgetStr = ismToSpendElement.InnerText.TrimStart('£').Replace(',', '.');
            return decimal.Parse(remainingBudgetStr, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
