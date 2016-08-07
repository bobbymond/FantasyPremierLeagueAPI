using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FantasyPremierLeagueApi.Model.Player
{
    [Serializable]
    public class PlayerSummaryStats
    {
        [JsonProperty("history_past")] public PlayerSeasonPerformance[] PastSeasonPerformance { get; set; }
    }
}
