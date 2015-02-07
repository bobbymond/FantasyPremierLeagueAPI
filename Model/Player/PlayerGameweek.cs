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
using System.Globalization;
using FantasyPremierLeagueApi.Model.Club;

namespace FantasyPremierLeagueApi.Model.Player
{
    [Serializable]
    public class PlayerGameweek
    {
        public int          Gameweek            { get; set; }
        public Result       Result              { get; set; }
        public int          MinutesPlayed       { get; set; }
        public int          GoalsScored         { get; set; }
        public int          Assists             { get; set; }
        public int          CleanSheets         { get; set; }
        public int          GoalsConceded       { get; set; }
        public int          OwnGoals            { get; set; }
        public int          PenaltiesSaved      { get; set; }
        public int          PenaltiesMissed     { get; set; }
        public int          YellowCards         { get; set; }
        public int          RedCards            { get; set; }
        public int          Saves               { get; set; }
        public int          Bonus               { get; set; }
        public int          EASportsPPI         { get; set; }
        public int          NetTransers         { get; set; }
        public int          Value               { get; set; }
        public int          Points              { get; set; }

        public PlayerGameweek(IClub club, object[] stats)
        {
            var koTime = (string)stats[0];
            var koDateTime = DateTime.ParseExact(koTime, "dd MMM HH:mm", CultureInfo.InvariantCulture);
            Gameweek = (int) (long) stats[1];
            var resultStr = (string)stats[2];
            Result = new Result(club, resultStr, koDateTime);
            MinutesPlayed = (int) (long)  stats[3];
            GoalsScored = (int) (long) stats[4];
            Assists = (int) (long) stats[5];
            CleanSheets = (int) (long)  stats[6];
            GoalsConceded = (int) (long)  stats[7];
            OwnGoals = (int) (long)  stats[8];
            PenaltiesSaved = (int) (long) stats[9];
            PenaltiesMissed = (int) (long)  stats[10];
            YellowCards = (int) (long)  stats[11];
            RedCards = (int) (long)  stats[12];
            Saves = (int) (long)  stats[13];
            Bonus = (int) (long)  stats[14];
            EASportsPPI = (int) (long)  stats[15];
            NetTransers = (int) (long)  stats[16];
            Value = (int) (long)  stats[17];
            Points = (int) (long) stats[19];
        }

    }
}
