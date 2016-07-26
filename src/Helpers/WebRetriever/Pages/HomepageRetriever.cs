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
using System.Net;
using HtmlAgilityPack;
using FantasyPremierLeagueApi.Helpers.Logger;
using Newtonsoft.Json.Linq;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever.Page
{
    public class HomepageRetriever
    {
        private const   string  EVENTS_URL      = "https://fantasy.premierleague.com/drf/events";
        private         ILogger _logger;

        public HomepageRetriever(ILogger logger)
        {
            _logger = logger;
        }

        public int GetCurrentGameweek(CookieContainer cookies)
        {
            var requester = new WebPageRequester(_logger);
            var json = requester.Get(EVENTS_URL, ref cookies);
            var events = JArray.Parse(json);
            if (events != null)
            {
                foreach (var obj in events.OfType<JObject>())
                {
                    var nr = obj.Property("id").Value.ToObject<int>();
                    var finished = obj.Property("finished").Value.ToObject<bool>();
                    if (!finished)
                        return nr;
                }
            }
            return 0;
        }

    }
}
