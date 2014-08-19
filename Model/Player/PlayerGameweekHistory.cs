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
using Newtonsoft.Json;
using System.Runtime.Serialization;
using FantasyPremierLeagueApi.Model.Club;

namespace FantasyPremierLeagueApi.Model.Player
{
    [Serializable]
    public class PlayerGameweekHistory
    {
        public List<PlayerGameweek> Gameweeks { get; private set; }

        public int MinutesPlayed { get; private set; }
        public int Goals { get; private set; }
        public int Assists { get; private set; }
        public int Conceded { get; private set; }
        public int CleanSheets { get; private set; }
        public int PenaltySaves { get; private set; }
        public int PenaltiesMissed { get; private set; }
        public int YellowCards { get; private set;  }
        public int RedCards { get; private set; }
        public int Saves { get; private set; }
        public int Bonus { get; private set; }
        public int GamesPlayed { get; private set; }
        public int OwnGoals { get; private set; }

        public PlayerGameweekHistory(IClub club, List<object[]> RawGameweeks)
        {
            Gameweeks = new List<PlayerGameweek>();
            MinutesPlayed = 0;
            Goals = 0;
            Assists = 0;
            Conceded = 0;
            CleanSheets = 0;
            PenaltySaves = 0;
            PenaltiesMissed = 0;
            YellowCards = 0;
            RedCards = 0;
            Saves = 0;
            Bonus = 0;

            foreach (var gw in RawGameweeks)
            {
                var playerGw = new PlayerGameweek(club, gw);
                Gameweeks.Add(playerGw);

                // calculate totals
                MinutesPlayed += playerGw.MinutesPlayed;
                Goals += playerGw.GoalsScored;
                Assists += playerGw.Assists;
                Conceded += playerGw.GoalsConceded;
                CleanSheets += playerGw.CleanSheets;
                PenaltySaves += playerGw.PenaltiesSaved;
                PenaltiesMissed += playerGw.PenaltiesMissed;
                YellowCards += playerGw.YellowCards;
                RedCards += playerGw.RedCards;
                Saves += playerGw.Saves;
                Bonus += playerGw.Bonus;
                OwnGoals += playerGw.OwnGoals;
            }

            GamesPlayed = RawGameweeks.Count();
        }

    }
}
