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
using FantasyPremierLeagueApi.Model.Club;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.Pages
{
    public class PlayerStatsRetriever
    {
        private const    string                 ELEMENTS_PAGE       = "https://fantasy.premierleague.com/drf/elements";
        private const    string                 PLAYER_HISTORY_PAGE = "https://fantasy.premierleague.com/drf/element-summary/";

        private          ILogger                _logger;
        private          JArray                 _jsonData;
        private readonly Dictionary<int, Team>  _teamsMap;

        public PlayerStatsRetriever(ILogger logger, TeamRetriever teamRetriever = null)
        {
            _logger = logger;
            teamRetriever = teamRetriever ?? new TeamRetriever(_logger);
            _teamsMap = new Dictionary<int, Team>();

            var teams = teamRetriever.GetAllTeams();
            foreach (var team in teams)
                _teamsMap[team.Id] = team;
            
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
                    var elementType = int.Parse(rawStats.ElementTypeString) - 1;
                    var position = (Enums.Position) elementType;

                    Team club;
                    if(!_teamsMap.TryGetValue(rawStats.TeamId, out club))
                        throw new ApplicationException("Unknown team id " + rawStats.TeamId);

                    var pastSeasons = GetPlayerPastSeasonPerformance(rawStats.Id);

                    switch (position)
                    {
                        case Enums.Position.Goalkeeper:
                            yield return new Goalkeeper(rawStats, club, pastSeasons, null);
                            break;
                        case Enums.Position.Defender:
                            yield return new Defender(rawStats, club, pastSeasons, null);
                            break;
                        case Enums.Position.Midfielder:
                            yield return new Midfielder(rawStats, club, pastSeasons, null);
                            break;
                        case Enums.Position.Forward:
                            yield return new Forward(rawStats, club, pastSeasons, null);
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

        private IEnumerable<PlayerSeasonPerformance> GetPlayerPastSeasonPerformance(int playerId)
        {
            var requester = new WebPageRequester(_logger);
            CookieContainer cookies = null;
            var json = requester.Get(PLAYER_HISTORY_PAGE + playerId.ToString(), ref cookies);
            var playerStats = JsonConvert.DeserializeObject<PlayerSummaryStats>(json);
            return playerStats.PastSeasonPerformance;
        }
    }
}
