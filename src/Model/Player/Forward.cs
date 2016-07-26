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
using System.Text;
using FantasyPremierLeagueApi.Model.Club;

namespace FantasyPremierLeagueApi.Model.Player
{
    [Serializable]
    public class Forward : Player
    {
        private const int m_PointsPerGoal = 4;
        private const int m_PointsPerCleanSheet = 0;
        private const int m_PointPer2GoalsConceded = 0;

        public Forward(RawPlayerStats stats, IClub club) : base(stats, club) {}

        override public int PointsPerGoal
        {
            get { return m_PointsPerGoal; }
        }

        override public int PointsPerCleanSheet
        {
            get { return m_PointsPerCleanSheet; }
        }

        override public int PointsPer2GoalsConceded
        {
            get { return m_PointPer2GoalsConceded; }
        }

    }
}
