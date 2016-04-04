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
using System.Globalization;

namespace FantasyPremierLeagueApi.Model
{
    [Serializable]
    public enum Venue
    {
        Home,
        Away
    }

    [Serializable]
    public class Fixture
    {
        private IClub m_team;
        private Venue m_venue;
        private IClub m_opposition;
        private DateTime m_kickOff;

        public Fixture(IClub team, IClub opposition, Venue v, DateTime ko)
        {
            m_team = team;
            m_venue = v;
            m_opposition = opposition;
            m_kickOff = ko;
        }

        /// <summary>
        /// Expects a 3 element array e.g.
        /// 
        /// [
        ///   "13 Apr 15:00",
        ///   "Gameweek 33",
        ///   "Norwich (H)"
        /// ]
        /// 
        /// </summary>
        /// <param name="team"></param>
        /// <param name="fixtureInfo"></param>
        public Fixture(IClub team, string[] fixtureInfo)
        {
            m_team = team;

            Match oppositionMatch = Regex.Match(fixtureInfo[2], @"(.+)\((H|A)\)", RegexOptions.Singleline);

            m_opposition = Clubs.GetClubFromName(oppositionMatch.Groups[1].Value.Trim());
            m_venue = ParseVenue(oppositionMatch.Groups[2].Value.Trim());
            m_kickOff = DateTime.ParseExact(fixtureInfo[0], "dd MMM HH:mm", CultureInfo.InvariantCulture);
        }

        public IClub Team { get { return m_team; } }
        public Venue Venue { get { return m_venue; } }
        public IClub Opposition { get { return m_opposition; } }
        public DateTime KickOff { get { return m_kickOff; } }

        public static Venue ParseVenue(string venue)
        {
            switch (venue)
            {
                case "H":
                case "(H)":
                    return Venue.Home;
                case "A":
                case "(A)":
                    return Venue.Away;
                default:
                    throw new ArgumentException("Unknown venue type " + venue);
            }
        }

        public string ToFriendlyString()
        {
            IClub home_team = m_venue == Venue.Home ? m_team : m_opposition;
            IClub away_team = m_venue == Venue.Home ? m_opposition : m_team;


            return String.Format("{0}: {1} v {2}", m_kickOff.ToString("dd/MM"), home_team.ToString(), away_team.ToString());
        }

        public string ToBiasedFriendlyString()
        {
            return String.Format("v {1} ({2}) - {0}", m_kickOff.ToString("dd/MM"), m_opposition.ToString(), m_venue.ToString());
        
        }

        public override bool Equals(object obj)
        {
            Fixture f2 = obj as Fixture;

            if (f2 == null)
                return base.Equals(obj);
            else
                return (f2.Team == m_team && f2.Opposition == m_opposition && f2.m_venue == m_venue);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + m_team.GetHashCode();
                hash = hash * 23 + m_opposition.GetHashCode();
                hash = hash * 23 + m_venue.GetHashCode();
                return hash;
            }
        }
    }
}
