using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyPremierLeagueApi.Model.Player
{
    [Serializable]
    public class PlayerSeasonPerformance
    {
        public long MinutesPlayed        { get; private set; }
        public long GoalsScored          { get; private set; }
        public long CleanSheets          { get; private set; }
        public long GoalsConceded        { get; private set; }
        public long OwnGoals             { get; private set; }
        public long PenaltiesSaved       { get; private set; }
        public long PenaltiesMissed      { get; private set; }
        public long YellowCards          { get; private set; }
        public long RedCards             { get; private set; }
        public long Saves                { get; private set; }
        public long Bonus                { get; private set; }
        public long EASportsPPI          { get; private set; }
        public long BonusPointsSystem    { get; private set; }

        public PlayerSeasonPerformance(object[] statsFromSite)
        {
            MinutesPlayed       = (long) statsFromSite[1];
            GoalsScored         = (long)statsFromSite[2];
            CleanSheets         = (long)statsFromSite[3];
            GoalsConceded       = (long)statsFromSite[4];
            OwnGoals            = (long)statsFromSite[5];
            PenaltiesSaved      = (long)statsFromSite[6];
            PenaltiesMissed     = (long)statsFromSite[7];
            YellowCards         = (long)statsFromSite[8];
            RedCards            = (long)statsFromSite[9];
            Saves               = (long)statsFromSite[10];
            Bonus               = (long)statsFromSite[11];
            EASportsPPI         = (long)statsFromSite[12];
            BonusPointsSystem   = (long)statsFromSite[13];
        }
    }
}
