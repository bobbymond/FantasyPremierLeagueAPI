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
using FantasyPremierLeagueApi.Helpers.Logger;
using FantasyPremierLeagueApi.Helpers.WebRetriever.Page;
using FantasyPremierLeagueApi.Helpers.WebRetriever;
using FantasyPremierLeagueApi.Model;
using FantasyPremierLeagueApi.Model.Club;
using FantasyPremierLeagueApi.Helpers.WebRetriever.Pages;
using FantasyPremierLeagueApi.Model.Player;

namespace FantasyPremierLeagueApi.Api
{
    public class FantasyPremierLeagueApi
    {
        private         ILogger                                     _logger;
        private         CookieContainer                             _session;
        private         Dictionary<int, Player>                     _allPlayersCache;
        private         string                                      _username;
        private         string                                      _password;

        private         TransferPageRetriever                       _transferPageRetriever;

        public FantasyPremierLeagueApi(string username, string password, ILogger logger)
        {
            _logger = logger;
            _allPlayersCache = new Dictionary<int, Player>();
            _username = username;
            _password = password;
        }

        public FantasyPremierLeagueApi(string username, string password) : this(username, password, new ConsoleLogger()) { }

        /// <returns>The current gameweek as defined by Fantasy Premier League</returns>
        public int GetGameweekNumber()
        {
            CheckAndCreateSession();
            var reader = new HomepageRetriever(_logger);
            return reader.GetCurrentGameweek(_session);
        }

        /// <summary>This method returns a map of club to their league performance for the specified season</summary>
        /// <param name="season">A string in the form 2012-2013</param>
        /// <returns>A dictionary mapping club to their ClubSeasonPerformance object</returns>
        public Dictionary<IClub, ClubSeasonPerformance> GetClubSeasonPerformances(string season = "2012-2013")
        {
            CheckAndCreateSession();
            var reader = new LeagueTableRetriever(_logger);
            return reader.GetLeagueTable(_session, season)
                .GroupBy(perf => perf.Club)
                .ToDictionary(grp => grp.Key, grp => grp.Single());
                    
        }

        /// <summary>
        /// Returns all stats for this season per player
        /// </summary>
        public IEnumerable<Player> GetAllPlayers()
        {
            var reader = new PlayerStatsRetriever(_logger);

            for (int i = 1; i <= PlayerStatsRetriever.MAXPLAYER_ID; i++)
            {
                try
                {
                    if (!_allPlayersCache.ContainsKey(i))
                    {
                        var player = reader.GetPlayerStats(i);
                        _allPlayersCache.Add(i, player);
                    }
                }
                catch (Exception e)
                {
                    // some of the ids may throw - only log to debug logger
                    _logger.WriteDebugMessage(string.Format("Error getting player stats for player id {0}: {1}\r\n{2}", i, e.Message, e.StackTrace));
                }
            }


            return _allPlayersCache.Values;
        }

        /// <summary>
        /// Return a single player's stats
        /// </summary>
        /// <param name="id">The player id (int)</param>
        /// <param name="reader">(optional)</param>
        /// <returns>Player stats</returns>
        public Player GetPlayer(int id, PlayerStatsRetriever reader = null)
        {
            if (reader == null)
                reader = new PlayerStatsRetriever(_logger);

            if (!_allPlayersCache.ContainsKey(id))
            {
                var player = reader.GetPlayerStats(id);
                _allPlayersCache.Add(id, player);
            }

            return _allPlayersCache[id];
        }

        /// <summary>
        /// Returns a dictionary mapping 15 player stat objects to sale values, representing the currently selected team of the logged on user
        /// </summary>
        /// <returns>An Dictionary mapping 15 player objects to their sale price</returns>
        public Dictionary<Player, int> GetMySquad()
        {
            var result = new Dictionary<Player, int>();

            CheckAndCreateSession();
            var myTeamReader = GetTransferPageRetriever();
            var playerReader = new PlayerStatsRetriever(_logger);
            var playerIdsToValues = myTeamReader.GetMyTeamTransferValues();

            foreach(var playerId in playerIdsToValues.Keys)
            {
                var player = GetPlayer(playerId, playerReader);
                result[player] = playerIdsToValues[playerId];
            }

            return result;
        }

        /// <summary>
        /// Returns the amount of money currently available for transfers
        /// </summary>
        public decimal GetRemainingBudget()
        {
            CheckAndCreateSession();
            var transferPageReader = GetTransferPageRetriever();
            return transferPageReader.GetRemainingBudget();
        }


        #region Private Methods

        private TransferPageRetriever GetTransferPageRetriever()
        {
            if (_transferPageRetriever == null)
            {
                CheckAndCreateSession();
                _transferPageRetriever = new TransferPageRetriever(_logger, _session);
            }

            return _transferPageRetriever;
        }

        private void CheckAndCreateSession()
        {
            if (_session == null)
            {
                _logger.WriteInfoMessage("Creating Session");
                var authenticator = new FantasyPremierLeagueAuthenticator(_logger);
                _session = authenticator.Authenticate(_username, _password);
            }
        }

        #endregion
    }
}
