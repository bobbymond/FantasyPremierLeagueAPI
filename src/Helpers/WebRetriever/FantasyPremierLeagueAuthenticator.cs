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
using System.Diagnostics;
using FantasyPremierLeagueApi.Helpers.Logger;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever
{
    public class FantasyPremierLeagueAuthenticator
    {
        private const string _URL_GETSESSIONCOOKIES1    = "https://users.premierleague.com";
        private const string _URL_LOGIN                 = "https://users.premierleague.com/PremierUser/j_spring_security_check";
        private const string _FIELD_USERNAME            = "j_username";
        private const string _FIELD_PASSWORD            = "j_password";

        private ILogger      _logger;

        public FantasyPremierLeagueAuthenticator(ILogger logger)
        {
            _logger = logger;
        }

        public CookieContainer Authenticate(string username, string password)
        {
            try
            {
                var requester = new WebPageRequester(_logger);

                _logger.WriteInfoMessage("Authenticate - Getting session cookies");
                var cookies = new CookieContainer();
                requester.Get(_URL_GETSESSIONCOOKIES1, ref cookies);

                if (cookies == null || cookies.Count < 2)
                    throw new Exception("Session cookies not set"); 
               
                _logger.WriteInfoMessage("Authenticate - Authenticating");

                var parameters = string.Format("{0}={1}&{2}={3}", _FIELD_USERNAME, username, _FIELD_PASSWORD, password);
                requester.Post(_URL_LOGIN, parameters, ref cookies);

                if (cookies.Count < 3)
                    throw new Exception("Authentication failed for user " + username);

                return cookies;
            }
            catch (Exception e)
            {
                _logger.WriteErrorMessage("Authenticate - Error occurred", e);
            }

            return null;
        }
    }
}
