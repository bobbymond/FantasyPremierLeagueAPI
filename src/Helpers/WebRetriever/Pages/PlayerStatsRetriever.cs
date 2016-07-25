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
using Newtonsoft.Json.Linq;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.Pages
{
    public class PlayerStatsRetriever
    {
        private const   string  ELEMENTS_PAGE   = "https://fantasy.premierleague.com/drf/elements";
        private         ILogger _logger;
        private         JArray  _jsonData;

        public PlayerStatsRetriever(ILogger logger)
        {
            _logger = logger;

            var requester = new WebPageRequester(_logger);
            CookieContainer cookies = null;
            var json = requester.Get(ELEMENTS_PAGE, ref cookies);
            _jsonData = JArray.Parse(json);
        }

        public IEnumerable<Player> GetAllPlayerStats()
        {
            if (_jsonData != null)
            {
                foreach (var token in _jsonData)
                {
                    var rawStats = token.ToObject<RawPlayerStats>();

                    var position = (Enums.Position)Enum.Parse(typeof(Enums.Position), rawStats.PositionString);

                    switch (position)
                    {
                        case Enums.Position.Goalkeeper:
                            yield return new Goalkeeper(rawStats);
                            break;
                        case Enums.Position.Defender:
                            yield return new Defender(rawStats);
                            break;
                        case Enums.Position.Midfielder:
                            yield return new Midfielder(rawStats);
                            break;
                        case Enums.Position.Forward:
                            yield return new Forward(rawStats);
                            break;
                        default:
                            throw new ApplicationException("Unknown position " + position);
                    }
                }
            }
        }

        public Player GetPlayerStats(int playerId)
        {
            var player = GetAllPlayerStats().FirstOrDefault(x => x.Id == playerId);
            return player;
        }
    }
}
