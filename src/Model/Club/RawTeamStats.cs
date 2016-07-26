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
using Newtonsoft.Json;

namespace FantasyPremierLeagueApi.Model.Club
{
    [Serializable]
    public class RawTeamStats
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("short_name")]
        public string ShortCode { get; set; }

        [JsonProperty("unavailable")]
        public bool Unavailable { get; set; }

        //[JsonProperty("form")]
        //public double Form { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("played")]
        public int Played { get; set; }

        [JsonProperty("win")]
        public int Win { get; set; }

        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("loss")]
        public int Loss { get; set; }

        [JsonProperty("strength")]
        public int Strength { get; set; }

        [JsonProperty("strength_overall_home")]
        public int StrengthOverallHome { get; set; }

        [JsonProperty("strength_overall_away")]
        public int StrengthOverallAway { get; set; }

        [JsonProperty("strength_attack_home")]
        public int StrengthAttackHome { get; set; }

        [JsonProperty("strength_attack_away")]
        public int StrengthAttackAway { get; set; }

        [JsonProperty("strength_defence_home")]
        public int StrengthDefenceHome { get; set; }

        [JsonProperty("strength_defence_away")]
        public int StrengthDefenceAway { get; set; }

    }
}
