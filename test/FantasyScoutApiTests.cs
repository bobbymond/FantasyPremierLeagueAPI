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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FantasyPremierLeagueApi;
using FantasyPremierLeagueApi.Helpers.Logger;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using FantasyPremierLeagueApi.Model.Club;

namespace FantasyPremierLeagueApi
{
    [TestClass()]
    public class FantasyScoutApiTests
    {
        private static ConsoleLogger _logger = new ConsoleLogger(TestConstants.TESTS_LOGGER_LEVEL);

        [TestMethod]
        public void TestGetExpectedTeamNews()
        {
            var api = new FantasyScoutApi(_logger);

            var expectedTeams = api.GetNextWeekStartingElevenByTeam();

            _logger.WriteInfoMessage("Expected teams: " +
                    JsonConvert.SerializeObject(expectedTeams));

             Assert.IsTrue(expectedTeams.Sum(team => team.Value.Count()) > 0, "No expected players found");

            // test serialize
            var formatter = new BinaryFormatter();
            using (var fs = new MemoryStream())
            {
                formatter.Serialize(fs, expectedTeams);
            }
        }

        [TestMethod]
        public void TestGetExpectedReturnDates()
        {
            var api = new FantasyScoutApi(_logger);

            var expectedReturnDates = api.GetExpectedReturnDatesByTeam();

        }
    }
}
