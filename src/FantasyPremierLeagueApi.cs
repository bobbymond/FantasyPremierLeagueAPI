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
        protected       ILogger                                     Logger { get; private set; }

        private         CookieContainer                             _session;
        private         Dictionary<int, Player>                     _allPlayersCache;
        private         string                                      _username;
        private         string                                      _password;
        
        private         HomepageRetriever                           _homepageRetriever;
        private         LeagueTableRetriever                        _leagueTableRetriever;
        private         TeamRetriever                               _teamRetriever;
        private         PlayerStatsRetriever                        _playerStatsRetriever;
        private         TransferPageRetriever                       _transferPageRetriever;

        public FantasyPremierLeagueApi(string username, string password, ILogger logger)
        {
            Logger = logger;
            _allPlayersCache = new Dictionary<int, Player>();
            _username = username;
            _password = password;
        }

        public FantasyPremierLeagueApi(string username, string password) : this(username, password, new ConsoleLogger()) { }


        /// <returns>The current gameweek as defined by Fantasy Premier League</returns>
        public int GetGameweekNumber()
        {
            //CheckAndCreateSession();
            var reader = GetHomepageRetriever();
            var result = reader.GetCurrentGameweek(_session);
            return result;
        }

        /// <summary>This method returns a map of club to their league performance for the specified season</summary>
        /// <param name="season">A string in the form 2012-2013</param>
        /// <returns>A dictionary mapping club to their ClubSeasonPerformance object</returns>
        public Dictionary<IClub, ClubSeasonPerformance> GetClubSeasonPerformances(string season = "2015-2016")
        {
            CheckAndCreateSession();
            var reader = GetLeagueTableRetriever();
            var result = reader.GetLeagueTable(_session, season)
                .GroupBy(perf => perf.Club)
                .ToDictionary(grp => grp.Key, grp => grp.Single());
            return result;

        }

        /// <summary>
        /// Returns all stats for this season per player
        /// </summary>
        public IEnumerable<Player> GetAllPlayers()
        {
            var reader = GetPlayerStatsRetriever();
            var players = reader.GetAllPlayerStats();

            if (players != null)
            {
                foreach (var player in players)
                {
                    try
                    {
                        if (!_allPlayersCache.ContainsKey(player.Id))
                        {
                            _allPlayersCache.Add(player.Id, player);
                        }
                    }
                    catch (ApplicationException ae)
                    {
                        Logger.WriteErrorMessage(string.Format("Error getting player stats for player id {0}: {1}\r\n{2}", player.Id, ae.Message, ae.StackTrace));
                    }
                    catch (Exception e)
                    {
                        // some of the ids may throw - only log to debug logger
                        Logger.WriteDebugMessage(string.Format("Error getting player stats for player id {0}: {1}\r\n{2}", player.Id, e.Message, e.StackTrace));
                    }
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
                reader = GetPlayerStatsRetriever();

            if (!_allPlayersCache.ContainsKey(id))
            {
                var player = reader.GetPlayerStats(id);
                _allPlayersCache.Add(id, player);
            }

            var result = _allPlayersCache[id];
            return result;
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
            var playerReader = GetPlayerStatsRetriever();
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
            var result = transferPageReader.GetRemainingBudget();
            return result;
        }


        #region Private Methods

        protected virtual HomepageRetriever GetHomepageRetriever()
        {
            if (_homepageRetriever == null)
            {
                _homepageRetriever = new HomepageRetriever(Logger);
            }

            return _homepageRetriever;
        }

        protected virtual LeagueTableRetriever GetLeagueTableRetriever()
        {
            if (_leagueTableRetriever == null)
            {
                _leagueTableRetriever = new LeagueTableRetriever(Logger);
            }

            return _leagueTableRetriever;
        }

        protected virtual TeamRetriever GetTeamRetriever()
        {
            if (_teamRetriever == null)
            {
                _teamRetriever = new TeamRetriever(Logger);
            }

            return _teamRetriever;
        }

        protected virtual PlayerStatsRetriever GetPlayerStatsRetriever()
        {
            if (_playerStatsRetriever == null)
            {
                var teamRetriever = GetTeamRetriever();
                _playerStatsRetriever = new PlayerStatsRetriever(Logger, teamRetriever);
            }

            return _playerStatsRetriever;
        }

        protected virtual TransferPageRetriever GetTransferPageRetriever()
        {
            if (_transferPageRetriever == null)
            {
                CheckAndCreateSession();
                _transferPageRetriever = new TransferPageRetriever(Logger, _session);
            }

            return _transferPageRetriever;
        }

        protected virtual void CheckAndCreateSession()
        {
            if (_session == null)
            {
                Logger.WriteInfoMessage("Creating Session");
                //var authenticator = new FantasyPremierLeagueAuthenticator(Logger);
                var authenticator = new FantasyPremierLeague2016Authenticator(Logger);
                _session = authenticator.Authenticate(_username, _password);

                if (_session == null)
                    throw new ApplicationException("Failed to create session");
            }
        }

        #endregion
    }
}
