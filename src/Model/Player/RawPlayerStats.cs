﻿#region license
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
using Newtonsoft.Json;

namespace FantasyPremierLeagueApi.Model.Player
{
    [Serializable]
    public class RawPlayerStats
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("web_name")]
        public string Name { get; set; }

        [JsonProperty("team")]
        public int TeamId { get; set; }

        [JsonProperty("now_cost")]
        public int Value { get; set; }

        [JsonProperty("total_points")]
        public int Points { get; set; }

        [JsonProperty("points_per_game")]
        public float PointsPerGame { get; set; }

        [JsonProperty("status")]
        public string AvailabilityStatusString { get; set; }

        [JsonProperty("news")]
        public string News { get; set; }

        [JsonProperty("fixture_history")]
        public RawPlayerGameweekHistory GameweekHistory { get; set; }

        [JsonProperty("fixtures")]
        public RawFixtures Fixtures { get; set; }

        [JsonProperty("element_type")]
        public string ElementTypeString { get; set; }

        [JsonProperty("season_history")]
        public object[][] SeasonHistory { get; set; }

        [Serializable]
        public class RawPlayerGameweekHistory
        {
            [JsonProperty("all")]
            public List<object[]> RawGameweeks { get; set; }
        }

        [Serializable]
        public class RawFixtures 
        {
            [JsonProperty("all")]
            public List<string[]> AllFixtures { get; set; }
        }

    }
}
