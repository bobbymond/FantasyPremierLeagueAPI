using System;
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

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FantasyPremierLeagueApi.Helpers.Logger;

namespace FantasyPremierLeagueApi.Helpers.WebRetriever
{
    [TestClass()]
    public class FantasyPremierLeagueAuthenticatorTests
    {
        public const ConsoleLogger.LogLevel     TESTS_LOGGER_LEVEL  = ConsoleLogger.LogLevel.Info;
        public const string                     USERNAME            = "username@email.com";
        public const string                     PASSWORD            = "password";

        [TestMethod]
        public void TestAuthenticate()
        {
            var logger = new ConsoleLogger();
            var authenticator = new FantasyPremierLeagueAuthenticator(logger);

            var cookies = authenticator.Authenticate(USERNAME, PASSWORD);

            Assert.IsNotNull(cookies);
            Assert.IsTrue(cookies.Count > 0);
        }
    }
}
