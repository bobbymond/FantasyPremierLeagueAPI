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
using FantasyPremierLeagueApi.Model.Club;
using System.Text.RegularExpressions;

namespace FantasyPremierLeagueApi.Model
{
    [Serializable]
    public class Result
    {
        public Fixture  Fixture          { get; set; }
        public int      GoalsScored      { get; set; }
        public int      GoalsConceded    { get; set; }

        
        /// <param name="team"></param>
        /// <param name="resultString">string in the format "[OPPOSITION 3 letter code]([VENUE]) [FOR]-[AGAINST] " e.g. "SWA(H) 2-0"</param>
        /// <param name="ko"></param>
        public Result(IClub team, string resultString, DateTime ko)
        {
            Match fixture_match = Regex.Match(resultString, @"(.+)\((H|A)\)(.*)", RegexOptions.Singleline);
            Fixture = new Fixture(team, Clubs.GetClubFromShortcode(fixture_match.Groups[1].Value.Trim()), Fixture.ParseVenue(fixture_match.Groups[2].Value.Trim()), ko);

            var scoreString = fixture_match.Groups[3].Value.Trim();

            if(string.IsNullOrEmpty(scoreString)) // the game hasn't been played yet
            {
                GoalsScored = 0;
                GoalsConceded = 0;
            }
            else
            {
                Match scoreMatch = Regex.Match(scoreString, @"(\d)-(\d)", RegexOptions.Singleline);

                GoalsScored = int.Parse(scoreMatch.Groups[1].Value.Trim());
                GoalsConceded = int.Parse(scoreMatch.Groups[2].Value.Trim());
            }

        }
    }
}
