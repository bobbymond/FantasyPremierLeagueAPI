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
using Newtonsoft.Json;

namespace FantasyPremierLeagueApi.Model.Player
{
    [Serializable]
    public abstract class Player
    {
        private const int   m_PointsPerYellow         = -1;
        private const int   m_PointsPerRed            = -3;
        private const int   m_PointPerPenaltyMiss     = -2;
        private const int   m_PointsPerAssist         = 3;
        private const int   m_PointsPerStart          = 1;
        private const int   m_PointsPer60             = 1;
        private const int   m_PointsPerOwnGoal        = -2;

        private readonly RawPlayerStats _rawStats;
        private readonly IClub _club;


        public Player(RawPlayerStats stats, IClub club)
        {
            if (stats == null)
                throw new ArgumentNullException("stats");
            if (club == null)
                throw new ArgumentNullException("club");
            _rawStats = stats;
            _club = club;

            var elementType = int.Parse(stats.ElementTypeString) - 1;
            Position = (Enums.Position) elementType;
            
            switch(stats.AvailabilityStatusString.Trim())
            {
                case "a":
                    AvailabilityStatus = Enums.Status.Available;
                    break;
                case "i":
                    AvailabilityStatus = Enums.Status.Injured;
                    break;
                case "d":
                    AvailabilityStatus = Enums.Status.Doubtful;
                    break;
                case "n":
                    AvailabilityStatus = Enums.Status.Unavailable;
                    break;
                case "s":
                    AvailabilityStatus = Enums.Status.Suspended;
                    break;
                case "u":
                    AvailabilityStatus = Enums.Status.Unavailable;
                    break;
                default:
                    throw new ApplicationException("Unknown status: " + stats.AvailabilityStatusString);
            }

            //try
            //{
            //    GameweekHistory = new PlayerGameweekHistory(Club, stats.GameweekHistory.RawGameweeks);
            //}
            //catch (Exception e)
            //{
            //    throw new ApplicationException(string.Format("Unable to parse gameweek history for player '{0}'", stats.Name), e);
            //}

            //try
            //{
            //    if (stats.SeasonHistory != null && stats.SeasonHistory.Any())
            //    {
            //        PreviousSeasons = new Dictionary<string, PlayerSeasonPerformance>();

            //        foreach (var season in stats.SeasonHistory)
            //            PreviousSeasons.Add(((string)season[0]).Trim(), new PlayerSeasonPerformance(season));
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw new ApplicationException(string.Format("Unable to parse season history for player '{0}'", stats.Name), e);
            //}

            //Fixtures = stats.Fixtures.AllFixtures
            //        .Where(f => f[0] != "-")
            //        .GroupBy(f => int.Parse(f[1].Split(' ')[1])) // group by gameweek (f[1] in form "Gameweek x")
            //        .ToDictionary(
            //            grp => grp.Key,
            //            grp => grp.Select(f => new Fixture(Club, f)).ToArray()
            //         );
                    
        }

        public int                                          Id                  { get { return _rawStats.Id; } }
        public string                                       Name                { get { return _rawStats.Name; } }
        public int                                          Value               { get { return _rawStats.Value; } }
        public int                                          Points              { get { return _rawStats.Points; } }
        public float                                        PointsPerGame       { get { return _rawStats.PointsPerGame; } }
        public string                                       News                { get { return _rawStats.News; } }
        
        public IClub                                        Club                { get { return _club; } }
        public Enums.Status                                 AvailabilityStatus  { get; private set; }
        public PlayerGameweekHistory                        GameweekHistory     { get; private set; }        
        public Enums.Position                               Position            { get; private set; }
        public Dictionary<int,Fixture[]>                    Fixtures            { get; private set; }
        public Dictionary<string, PlayerSeasonPerformance>  PreviousSeasons     { get; private set; }     
        
        public int MinutesPlayed { get { return GameweekHistory.MinutesPlayed; } }
        public int Goals { get { return GameweekHistory.Goals; } }
        public int Assists { get { return GameweekHistory.Assists; } }
        public int Conceded { get { return GameweekHistory.Conceded; } }
        public int CleanSheets { get { return GameweekHistory.CleanSheets; } }
        public int PenaltySaves { get { return GameweekHistory.PenaltySaves; } }
        public int PenaltiesMissed { get { return GameweekHistory.PenaltiesMissed; } }
        public int YellowCards { get { return GameweekHistory.YellowCards; } }
        public int RedCards { get { return GameweekHistory.RedCards; } }
        public int Saves { get { return GameweekHistory.Saves; } }
        public int Bonus { get { return GameweekHistory.Bonus; } }
        public int GamesPlayed { get { return GameweekHistory.GamesPlayed; } }
        public int OwnGoals { get { return GameweekHistory.OwnGoals; } }

        abstract public int PointsPerGoal { get; }
        abstract public int PointsPerCleanSheet { get; }
        abstract public int PointsPer2GoalsConceded { get; }

        public int PointsPerAssist { get { return m_PointsPerAssist; } }
        public int PointsPerStart { get { return m_PointsPerStart; } }
        public int PointsPer60Mins { get { return m_PointsPer60; } }
        public int PointsPerOwnGoal { get { return m_PointsPerOwnGoal; } }
        public int PointsPerRed { get { return m_PointsPerRed; } }
        public int PointsPerYellow { get { return m_PointsPerYellow; } }
        public int PointsPerPenaltyMiss { get { return m_PointPerPenaltyMiss; } }

        public float AssistPerMinute
        {
            get { return ((MinutesPlayed == 0) ? 
                    0 : ((float) Assists / (float) MinutesPlayed)); }
        }

        public float AssistsPerGame
        {
            get
            {
                return ((GamesPlayed == 0) ?
                  0 : ((float)Assists / (float) GamesPlayed));
            }
        }

        public float GoalsPerMinute
        {
            get
            {
                return ((MinutesPlayed == 0) ?
                  0 : ((float)Goals / (float)MinutesPlayed));
            }
        }

        public float GoalsPerGame
        {
            get
            {
                return ((GamesPlayed == 0) ?
                  0 : ((float)Goals / (float)GamesPlayed));
            }
        }

        public float AverageBonus
        {
            get { return ((MinutesPlayed == 0) ? 
                    0 : ((float) Bonus / (float) GamesPlayed)); }
        }

        public float AverageAssistPointsPerGame
        {
            get
            {
                return ((MinutesPlayed == 0) ?
                  0 : ((float)Assists / (float)GamesPlayed) * PointsPerAssist);
            }
        }

        public float AverageGoalPointsPerGame
        {
            get
            {
                return ((MinutesPlayed == 0) ?
                  0 : ((float)Goals / (float)GamesPlayed) * PointsPerGoal);
            }
        }

        public float AverageCleanSheetPointsPerGame
        {
            get
            {
                return ((MinutesPlayed == 0) ?
                  0 : ((float)CleanSheets / (float)GamesPlayed) * (float) PointsPerCleanSheet);
            }
        }

        public float MeanConcededPerGame
        {
            get
            {
                return ((MinutesPlayed == 0) ?
                  0 : ((float)Conceded / (float)GamesPlayed));
            }
        }

        public float AverageMinutesPerGame
        {
            get
            {
                return (float)this.MinutesPlayed / (float)GamesPlayed;
            }
        }

        public float AverageYellowCardPointsPerGame
        {
            get { 

                if(MinutesPlayed == 0) return 0;

                float cards_per_game;

                if (MinutesPlayed < 90) cards_per_game = (float) YellowCards;
                else cards_per_game = ((float) YellowCards / (float) GamesPlayed);


                return cards_per_game * m_PointsPerYellow;
            }
        }

        public float AverageRedCardPointsPerGame
        {
            get
            {

                if (MinutesPlayed == 0) return 0;

                float cards_per_game;

                if (MinutesPlayed < 90) cards_per_game = (float)RedCards;
                else cards_per_game = ((float)RedCards / (float)GamesPlayed);


                return cards_per_game * m_PointsPerRed;
            }
        }

        public float AveragePenaltyMissPointsPerGame
        {
            get 
            {
                return ((MinutesPlayed == 0) ? 
                    0 : (((float) PenaltiesMissed / (float) GamesPlayed) * (float) m_PointPerPenaltyMiss)); 
            }
        }

        public float OwnGoalsPointsPerGame
        {
            get
            {
                return ((MinutesPlayed == 0) ?
                    0 : (((float)OwnGoals / (float)GamesPlayed) * (float)m_PointsPerOwnGoal)); 
            }
        }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null) return false;
            
            // If parameter cannot be cast to Player return false.
            var p = obj as Player;
            if (p == null) return false;
            
            // Return true if the fields match:
            return (Name == p.Name) && (Club == p.Club) && (Position == p.Position);
        }

        public override int GetHashCode()
        {
            var result = 17;
            result = result * 23 + Name.GetHashCode();
            result = result * 23 + Club.ShortCode.GetHashCode();

            return result;
        }
    }
}
