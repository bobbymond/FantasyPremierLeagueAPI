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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FantasyPremierLeagueApi.Model.Club;
using System.Globalization;
using System.Diagnostics;

namespace FantasyPremierLeagueApi.Model
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void TestResultConstructor()
        {
            var arsenal = Clubs.GetClubFromShortcode("ARS");
            var kickOff = DateTime.ParseExact("30 SEP 15:00", "dd MMM HH:mm", CultureInfo.InvariantCulture);
            var result = new Result(arsenal, "SWA(H) 2-0", kickOff);

            Console.WriteLine("Team: " + result.Fixture.Team);
            Console.WriteLine("Opposition: " + result.Fixture.Opposition);
            Console.WriteLine("Venue: " + result.Fixture.Venue);
            Console.WriteLine("Kick-off: " + result.Fixture.KickOff);
            Console.WriteLine("Scored: " + result.GoalsScored);
            Console.WriteLine("Conceded: " + result.GoalsConceded);

        }
    }
}
