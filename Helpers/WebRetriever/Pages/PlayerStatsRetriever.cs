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

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.Pages
{
    public class PlayerStatsRetriever
    {
        public  const   int     MAXPLAYER_ID        = 700;

        private const   string  _PLAYERSTATS_PAGE   = "http://fantasy.premierleague.com/web/api/elements";
        private         ILogger _logger;

        public PlayerStatsRetriever(ILogger logger)
        {
            _logger = logger;
        }

        public Player GetPlayerStats(int playerId)
        {
            var requester = new WebPageRequester(_logger);

            var url = string.Format("{0}/{1}/", _PLAYERSTATS_PAGE, playerId);

            CookieContainer cookies = null;
            var response = requester.Get(url, ref cookies);

            var rawStats = JsonConvert.DeserializeObject<RawPlayerStats>(response);

            var position = (Enums.Position)Enum.Parse(typeof(Enums.Position), rawStats.PositionString);

            switch (position)
            {
                case Enums.Position.Goalkeeper:
                    return new Goalkeeper(rawStats);
                case Enums.Position.Defender:
                    return new Defender(rawStats);
                case Enums.Position.Midfielder:
                    return new Midfielder(rawStats);
                case Enums.Position.Forward:
                    return new Forward(rawStats);
                default:
                    throw new ApplicationException("Unknown position " + position);
            }
        }
    }
}
