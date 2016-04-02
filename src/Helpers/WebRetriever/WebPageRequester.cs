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
using FantasyPremierLeagueApi.Helpers.Logger;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever
{
    public class WebPageRequester
    {
        private const string _HEADERS_ACCEPT            = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        private const string _HEADERS_ACCEPT_ENCODING   = "gzip, deflate";
        private const string _HEADERS_ACCEPT_LANGUAGE   = "en-gb,en;q=0.5";
        private const string _HEADERS_USER_AGENT        = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0"; // just a dummy user agent that looks like a real browser

        private ILogger _logger;

        public WebPageRequester(ILogger logger)
        {
            _logger = logger;
        }

        public string Post(string url, string data, ref CookieContainer refCookies)
        {
            return MakeRequest(url, "application/x-www-form-urlencoded", "POST", data, ref refCookies);
        }

        public string Get(string url, ref CookieContainer refCookies)
        {
            return MakeRequest(url, "text/html", "GET", null, ref refCookies);
        }

        #region Private Methods

        private string MakeRequest(string url, string contenttype, string method, string data, ref CookieContainer refCookies)
        {
            // create new request
            var req = (HttpWebRequest)System.Net.HttpWebRequest.Create(url);

            // add headers as per browser
            req.Accept                      = _HEADERS_ACCEPT;
            req.UserAgent                   = _HEADERS_USER_AGENT;
            req.Headers["Accept-Encoding"]  = _HEADERS_ACCEPT_ENCODING;
            req.Headers["Accept-Language"]  = _HEADERS_ACCEPT_LANGUAGE; 
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            // Set method/content type
            req.ContentType = contenttype;
            req.Method = method;

            // Add cookies
            if (refCookies == null)
                refCookies = new CookieContainer();
            req.CookieContainer = refCookies;
            
            if (data != null)
            {
                _logger.WriteDebugMessage("WebPageRequester.MakeRequest - Sending data");

                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data);
                req.ContentLength = bytes.Length;

                using (System.IO.Stream os = req.GetRequestStream())
                {
                    os.Write(bytes, 0, bytes.Length); //Push it out there
                    os.Close();
                }
            }

            _logger.WriteDebugMessage("WebPageRequester.MakeRequest - Reading response");

            using (System.Net.HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                if (resp == null)
                    _logger.WriteErrorMessage("WebPageRequester.MakeRequest - No response received");
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                var strResponse = sr.ReadToEnd().Trim();
               // _logger.WriteInfoMessage("WebPageRequester.MakeRequest - Response: " + strResponse);
                _logger.WriteDebugMessage("WebPageRequester.MakeRequest - Response Content Length: " + resp.ContentLength);
                _logger.WriteDebugMessage("WebPageRequester.MakeRequest - Response Content Encoding: " + resp.ContentEncoding);
                _logger.WriteDebugMessage("WebPageRequester.MakeRequest - Response Content Type: " + resp.ContentType);
                _logger.WriteDebugMessage("WebPageRequester.MakeRequest - Response Character Set: " + resp.CharacterSet);
                return strResponse;
            }
        }

        #endregion
    }
}
