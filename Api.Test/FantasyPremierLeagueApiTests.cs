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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FantasyPremierLeagueApi.Helpers.Logger;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace FantasyPremierLeagueApi.Api
{
    [TestClass()]
    public class FantasyPremierLeagueApiTests
    {
        private static ConsoleLogger _logger = new ConsoleLogger(TestConstants.TESTS_LOGGER_LEVEL);

        [TestMethod]
        public void TestGetGameweekNumber()
        {
            var api = new FantasyPremierLeagueApi(TestConstants.USERNAME, TestConstants.PASSWORD, _logger);

            var number = api.GetGameweekNumber();

            _logger.WriteInfoMessage("Current gameweek number is: " + number);
        }

        [TestMethod]
        public void TestGetClubSeasonPerformances()
        {
            var api = new FantasyPremierLeagueApi(TestConstants.USERNAME, TestConstants.PASSWORD, _logger);

            var lastSeason = api.GetClubSeasonPerformances("2014-2015");
            var thisSeason = api.GetClubSeasonPerformances("2013-2014");

            _logger.WriteInfoMessage("Last season's league performance by club: " +
                    JsonConvert.SerializeObject(lastSeason));

            _logger.WriteInfoMessage("This season's league performance by club: " +
                    JsonConvert.SerializeObject(thisSeason));

            // test serialize
            var formatter = new BinaryFormatter();
            using (var fs = new MemoryStream())
            {
                formatter.Serialize(fs, thisSeason);
            }
        }

        [TestMethod]
        public void TestGetAllPlayers()
        {
            var api = new FantasyPremierLeagueApi(TestConstants.USERNAME, TestConstants.PASSWORD, _logger);

            var allPlayers = api.GetAllPlayers();

            _logger.WriteInfoMessage("All player stats: " +
                    JsonConvert.SerializeObject(allPlayers));

            // test serialize
            var formatter = new BinaryFormatter();
            using (var fs = new MemoryStream())
            {
                formatter.Serialize(fs, allPlayers);
            }
        }

        [TestMethod]
        public void TestGetPlayer()
        {
            var api = new FantasyPremierLeagueApi(TestConstants.USERNAME, TestConstants.PASSWORD, _logger);

            var player = api.GetPlayer(23);

            _logger.WriteInfoMessage("Player 23 stats: " + JsonConvert.SerializeObject(player));

            // test serialize
            var formatter = new BinaryFormatter();
            using (var fs = new MemoryStream())
            {
                formatter.Serialize(fs, player);
            }
        }

        [TestMethod]
        public void TestGetMySquad()
        {
            var api = new FantasyPremierLeagueApi(TestConstants.USERNAME, TestConstants.PASSWORD, _logger);

            var allPlayers = api.GetMySquad();

            _logger.WriteInfoMessage("My squad stats: " +
                    JsonConvert.SerializeObject(allPlayers));

            // test serialize
            var formatter = new BinaryFormatter();
            using (var fs = new MemoryStream())
            {
                formatter.Serialize(fs, allPlayers);
            }
        }

        [TestMethod]
        public void TestGetRemainingBudget()
        {
            var api = new FantasyPremierLeagueApi(TestConstants.USERNAME, TestConstants.PASSWORD, _logger);

            var remainingBudget = api.GetRemainingBudget();

            _logger.WriteInfoMessage("Remaining budget: " + remainingBudget);

            // test serialize
            var formatter = new BinaryFormatter();
            using (var fs = new MemoryStream())
            {
                formatter.Serialize(fs, remainingBudget);
            }
        }

    }
}
