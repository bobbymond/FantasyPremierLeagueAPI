#region license
// Copyright (c) 2013 Mark Hammond
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

namespace FantasyPremierLeagueApi.Helpers.Logger
{
    public class ConsoleLogger : ILogger
    {
        public enum LogLevel
        {
            Debug   = 0,
            Info    = 1,
            Error   = 2
        }

        private LogLevel _level;

        public ConsoleLogger() : this(LogLevel.Debug) { }

        public ConsoleLogger(LogLevel logLevel)
        {
            _level = logLevel;
        }

        public void WriteDebugMessage(string message)
        {
            if(_level <= LogLevel.Debug)
                Console.WriteLine("DEBUG: " + message);
        }

        public void WriteInfoMessage(string message)
        {
            if(_level <= LogLevel.Info)
                Console.WriteLine("INFO: " + message); 
        }

        public void WriteErrorMessage(string message)
        {
            if(_level <= LogLevel.Error)
                Console.WriteLine("ERROR: " + message);
        }

        public void WriteErrorMessage(string message, Exception e)
        {
           WriteErrorMessage(string.Format("{0}\r\nEXCEPTION:{1}\r\nTRACE:{2}", message, e.Message, e.StackTrace));
        }
    }
}
