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
using FantasyPremierLeagueApi.Helpers.Logger;
using FantasyPremierLeagueApi.Model.Club;
using FantasyPremierLeagueApi.Helpers.WebRetriever.FantasyScout;

namespace FantasyPremierLeagueApi.Api
{
    public class FantasyScoutApi
    {
        private         ILogger                                     _logger;

        public FantasyScoutApi(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns a dictionary mapping clubs to their expected starting XI players, as scraped from www.fantasyfootballscout.co.uk
        /// </summary>
        /// <returns>Dictionary mapping Club -> Set of player name</returns>
        public Dictionary<IClub, IEnumerable<string>> GetNextWeekStartingElevenByTeam()
        {
            var reader = new TeamNewsRetriever(_logger);

            return reader.GetTeamNews();  
        }

        /// <summary>
        /// Returns a dictionary mapping clubs to a dictionary mapping player names to expected return dates, as scraped from www.fantasyfootballscout.co.uk
        /// 
        /// Excludes players with unknown return dates
        /// </summary>
        /// <returns>Dictionary mapping Club -> [Dictionary mapping player name -> expected return date]</returns>
        public Dictionary<IClub, Dictionary<string, DateTime>> GetExpectedReturnDatesByTeam()
        {
            var reader = new InjuryNewRetriever(_logger);

            return reader.GetInjuryNews();
        }
    }
}
