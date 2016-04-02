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
        public long Assists              { get; private set; }
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
            Assists             = (long)statsFromSite[3];
            CleanSheets         = (long)statsFromSite[4];
            GoalsConceded       = (long)statsFromSite[5];
            OwnGoals            = (long)statsFromSite[6];
            PenaltiesSaved      = (long)statsFromSite[7];
            PenaltiesMissed     = (long)statsFromSite[8];
            YellowCards         = (long)statsFromSite[9];
            RedCards            = (long)statsFromSite[10];
            Saves               = (long)statsFromSite[11];
            Bonus               = (long)statsFromSite[12];
            EASportsPPI         = (long)statsFromSite[13];
            BonusPointsSystem   = (long)statsFromSite[14];
        }
    }
}
