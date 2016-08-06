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

namespace FantasyPremierLeagueApi.Model.Club
{
    [Serializable]
    public class Team : IClub
    {
        private readonly RawTeamStats _stats;

        public Team(RawTeamStats stats)
        {
            if (stats == null)
                throw new ArgumentNullException(nameof(stats));
            _stats = stats;
        }

        public int Id { get { return _stats.Id; } }
        public string FullName  { get { return _stats.Name; } }
        public string ShortCode { get { return _stats.ShortCode; } }
        public virtual List<string> AltNames { get { return null; } }
        

        public RawTeamStats GetStats()
        {
            return _stats;
        }


        public override int GetHashCode()
        {
            return ShortCode.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var team = obj as Team;
            if (team == null) return false;
            return ShortCode == team.ShortCode;
        }

        public override string ToString()
        {
            return ShortCode;
        }
    }
}
