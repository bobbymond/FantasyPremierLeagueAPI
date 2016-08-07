using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FantasyPremierLeagueApi.Model.Player
{
    [Serializable]
    public class PlayerSeasonPerformance
    {

        [JsonProperty("season_name")]       public string       SeasonName           { get;  set; }
        [JsonProperty("minutes")]           public long         MinutesPlayed        { get;  set; }
        [JsonProperty("goals_scored")]      public long         GoalsScored          { get;  set; }
        [JsonProperty("assists")]           public long         Assists              { get;  set; }
        [JsonProperty("clean_sheets")]      public long         CleanSheets          { get;  set; }
        [JsonProperty("goals_conceded")]    public long         GoalsConceded        { get;  set; }
        [JsonProperty("own_goals")]         public long         OwnGoals             { get;  set; }
        [JsonProperty("penalties_saved")]   public long         PenaltiesSaved       { get;  set; }
        [JsonProperty("penalties_missed")]  public long         PenaltiesMissed      { get;  set; }
        [JsonProperty("yellow_cards")]      public long         YellowCards          { get;  set; }
        [JsonProperty("red_cards")]         public long         RedCards             { get;  set; }
        [JsonProperty("saves")]             public long         Saves                { get;  set; }
        [JsonProperty("bonus")]             public long         Bonus                { get;  set; }
        [JsonProperty("ea_index")]          public long         EASportsPPI          { get;  set; }
        [JsonProperty("bps")]               public long         BonusPointsSystem    { get;  set; }

    }
}
