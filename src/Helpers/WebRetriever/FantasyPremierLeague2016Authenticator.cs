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
using HtmlAgilityPack;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever
{
    public class FantasyPremierLeague2016Authenticator
    {
        private const string _URL_LOGIN                 = "https://users.premierleague.com/accounts/login/";
        private const string _URL_LOGIN_CONFIRM         = "https://fantasy.premierleague.com/a/login?a={0}&state=success&r={1}&e=3600&s=read+write";
        private const string _FIELD_USERNAME            = "login";
        private const string _FIELD_PASSWORD            = "password";

        private ILogger      _logger;

        public FantasyPremierLeague2016Authenticator(ILogger logger)
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
                var html = requester.Get(_URL_LOGIN, ref cookies);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                //var body = htmlDoc.GetElementbyId("ismTeamDisplayGraphical");
                //var ismPitch = body.SelectSingleNode("//div[@class='ismPitch']");
                //var players = ismPitch.SelectNodes("//div[@class='ismPlayerContainer']");

                var loginForm = htmlDoc.DocumentNode.SelectSingleNode("//form[@class='ism-form__login-box']");
                var csrfHiddenInput = loginForm?.SelectSingleNode("//input[@name='csrfmiddlewaretoken']");
                var csrfmiddlewaretoken = csrfHiddenInput?.GetAttributeValue("value", null);


                _logger.WriteInfoMessage("Authenticate - Authenticating");
                var parameters = "";
                parameters += string.Format("{0}={1}&", "csrfmiddlewaretoken", csrfmiddlewaretoken);
                parameters += string.Format("{0}={1}&", _FIELD_USERNAME, username);
                parameters += string.Format("{0}={1}&", _FIELD_PASSWORD, password);
                parameters += string.Format("{0}={1}&", "app", "plfpl-web");
                parameters += string.Format("{0}={1}&", "redirect-uri", _URL_LOGIN);
                parameters = parameters.Trim('&');
                var response = requester.Post(_URL_LOGIN, parameters, ref cookies);




                _logger.WriteInfoMessage("Authenticate - Authenticating");
                var parameters2 = "";
                parameters2 += string.Format("{0}={1}&", "csrfmiddlewaretoken", csrfmiddlewaretoken);
                parameters2 += string.Format("{0}={1}&", _FIELD_USERNAME, username);
                parameters2 += string.Format("{0}={1}&", _FIELD_PASSWORD, password);
                parameters2 += string.Format("{0}={1}&", "app", "plfpl-web");
                parameters2 += string.Format("{0}={1}&", "redirect-uri", _URL_LOGIN);
                parameters2 = parameters2.Trim('&');
                var auth = "";
                var r = "";
                var confirmUrl = string.Format(_URL_LOGIN_CONFIRM, auth, r);
                var response2 = requester.Get(confirmUrl, ref cookies);


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
