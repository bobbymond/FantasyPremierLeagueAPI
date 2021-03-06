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
    public static class Clubs
    {
        /// <summary>
        /// Returns a club from its full name or one of its alternative names
        /// </summary>
        /// <param name="clubName"></param>
        /// <returns></returns>
        public static IClub GetClubFromName(string clubName)
        {
            if(clubName == null)
                throw new ArgumentNullException(clubName);

            IClub result;
            if (!clubsByFullName.TryGetValue(clubName, out result) && !clubsByAltName.TryGetValue(clubName, out result))
                throw new InvalidOperationException("Club not found: " + clubName);

            return result;
        }

        /// <summary>
        /// Returns a club from its short code
        /// </summary>
        /// <param name="clubShortCode"></param>
        /// <returns></returns>
        public static IClub GetClubFromShortcode(string clubShortCode)
        {
            if(clubShortCode == null)
                throw new ArgumentNullException(clubShortCode);

            IClub result;
            if (!clubsByShortCode.TryGetValue(clubShortCode, out result))
                throw new InvalidOperationException("Club not found: " + clubShortCode);

            return result;
        }


        #region All Clubs (Immutable)

        [Serializable]
        public sealed class Arsenal : AbstractClub
        {

            #region Singleton Pattern 

            private static readonly Arsenal _instance = new Arsenal();
   
            private Arsenal(){}

            public static Arsenal Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Arsenal"; } }
            public override string ShortCode { get { return "ARS"; } }

        }

        [Serializable]
        public sealed class AstonVilla : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly AstonVilla _instance = new AstonVilla();
   
            private AstonVilla(){}

            public static AstonVilla Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Aston Villa"; } }
            public override string ShortCode { get { return "AVL"; } }
        }

        [Serializable]
        public sealed class Bournemouth : AbstractClub
        {
            #region Singleton Pattern

            private static readonly Bournemouth _instance = new Bournemouth();

            private Bournemouth() { }

            public static Bournemouth Instance
            {
                get
                {
                    return _instance;
                }
            }

            #endregion

            public override string FullName { get { return "Bournemouth"; } }
            public override string ShortCode { get { return "BOU"; } }
        }

        [Serializable]
        public sealed class Burnley : AbstractClub
        {
            #region Singleton Pattern

            private static readonly Burnley _instance = new Burnley();

            private Burnley() { }

            public static Burnley Instance
            {
                get
                {
                    return _instance;
                }
            }

            #endregion

            public override string FullName { get { return "Burnley"; } }
            public override string ShortCode { get { return "BUR"; } }
        }


        [Serializable]
        public sealed class Cardiff : AbstractClub
        {
            #region Singleton Pattern

            private static readonly Cardiff _instance = new Cardiff();

            private Cardiff() { }

            public static Cardiff Instance
            {
                get
                {
                    return _instance;
                }
            }

            #endregion

            public override string FullName { get { return "Cardiff City"; } }
            public override string ShortCode { get { return "CAR"; } }
        }

        [Serializable]
        public sealed class Chelsea : AbstractClub
        {
             #region Singleton Pattern 

            private static readonly Chelsea _instance = new Chelsea();
   
            private Chelsea(){}

            public static Chelsea Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Chelsea"; } }
            public override string ShortCode { get { return "CHE"; } }
        }

        [Serializable]
        public sealed class CrystalPalace : AbstractClub
        {
            #region Singleton Pattern

            private static readonly CrystalPalace _instance = new CrystalPalace();

            private CrystalPalace() { }

            public static CrystalPalace Instance
            {
                get
                {
                    return _instance;
                }
            }

            #endregion

            public override string FullName { get { return "Crystal Palace"; } }
            public override string ShortCode { get { return "CRY"; } }
        }

        [Serializable]
        public sealed class Everton : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Everton _instance = new Everton();
   
            private Everton(){}

            public static Everton Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Everton"; } }
            public override string ShortCode { get { return "EVE"; } }
        }

        [Serializable]
        public sealed class Fulham : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Fulham _instance = new Fulham();
   
            private Fulham(){}

            public static Fulham Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Fulham"; } }
            public override string ShortCode { get { return "FUL"; } }
        }

        [Serializable]
        public sealed class Hull : AbstractClub
        {
            #region Singleton Pattern

            private static readonly Hull _instance = new Hull();

            private Hull() { }

            public static Hull Instance
            {
                get
                {
                    return _instance;
                }
            }

            #endregion

            public override string FullName { get { return "Hull City"; } }
            public override List<string> AltNames { get { return new List<string> { "Hull" }; } }
            public override string ShortCode { get { return "HUL"; } }
        }

        [Serializable]
        public sealed class Leicester : AbstractClub
        {
            #region Singleton Pattern

            private static readonly Leicester _instance = new Leicester();

            private Leicester() { }

            public static Leicester Instance
            {
                get
                {
                    return _instance;
                }
            }

            #endregion

            public override string FullName { get { return "Leicester City"; } }
            public override List<string> AltNames { get { return new List<string> { "Leicester" }; } }
            public override string ShortCode { get { return "LEI"; } }
        }

        [Serializable]
        public sealed class Liverpool : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Liverpool _instance = new Liverpool();
   
            private Liverpool(){}

            public static Liverpool Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Liverpool"; } }
            public override string ShortCode { get { return "LIV"; } }
        }

        [Serializable]
        public sealed class ManCity : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly ManCity _instance = new ManCity();
   
            private ManCity(){}

            public static ManCity Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Manchester City"; } }
            public override List<string> AltNames { get { return new List<string> { "Man City" }; } }
            public override string ShortCode { get { return "MCI"; } }
        }

        [Serializable]
        public sealed class ManUtd : AbstractClub
        {
             #region Singleton Pattern 

            private static readonly ManUtd _instance = new ManUtd();
   
            private ManUtd(){}
            public static ManUtd Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion   

            public override string FullName  { get { return "Manchester United"; } }
            public override List<string> AltNames { get { return new List<string> { "Man Utd" }; } }
            
            public override string ShortCode { get { return "MUN"; } }

        }

        [Serializable]
        public sealed class Newcastle : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Newcastle _instance = new Newcastle();
   
            private Newcastle(){}

            public static Newcastle Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Newcastle United"; } }
            public override List<string> AltNames { get { return new List<string> { "Newcastle" }; } }
            public override string ShortCode { get { return "NEW"; } }
        }

        [Serializable]
        public sealed class Norwich : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Norwich _instance = new Norwich();
   
            private Norwich(){}

            public static Norwich Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName { get { return "Norwich City"; } }
            public override List<string> AltNames { get { return new List<string> { "Norwich" }; } }
            public override string ShortCode { get { return "NOR"; } }
        }

        [Serializable]
        public sealed class QPR : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly QPR _instance = new QPR();
   
            private QPR(){}

            public static QPR Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Queens Park Rangers"; } }
            public override List<string> AltNames { get { return new List<string> { "QPR" }; } }
            public override string ShortCode { get { return "QPR"; } }
        }

        [Serializable]
        public sealed class Reading : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Reading _instance = new Reading();
   
            private Reading(){}

            public static Reading Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Reading"; } }
            public override string ShortCode { get { return "RDG"; } }
        }

        [Serializable]
        public sealed class Southampton : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Southampton _instance = new Southampton();
   
            private Southampton(){}

            public static Southampton Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Southampton"; } }
            public override string ShortCode { get { return "SOU"; } }

        }

        [Serializable]
        public sealed class Spurs : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Spurs _instance = new Spurs();
   
            private Spurs(){}

            public static Spurs Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Tottenham Hotspur"; } }
            public override List<string> AltNames { get { return new List<string> { "Tottenham", "Spurs" }; } }
            public override string ShortCode { get { return "TOT"; } }
        }

        [Serializable]
        public sealed class Stoke : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Stoke _instance = new Stoke();
   
            private Stoke(){}

            public static Stoke Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Stoke City"; } }
            public override List<string> AltNames { get { return new List<string> { "Stoke" }; } }
            public override string ShortCode { get { return "STK"; } }

        }

        [Serializable]
        public sealed class Sunderland : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Sunderland _instance = new Sunderland();
   
            private Sunderland(){}

            public static Sunderland Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Sunderland"; } }
            public override string ShortCode { get { return "SUN"; } }
        }

        [Serializable]
        public sealed class Swansea : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Swansea _instance = new Swansea();
   
            private Swansea(){}

            public static Swansea Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Swansea City"; } }
            public override List<string> AltNames { get { return new List<string> { "Swansea" }; } }
            public override string ShortCode { get { return "SWA"; } }
        }


        [Serializable]
        public sealed class Watford : AbstractClub
        {
            #region Singleton Pattern

            private static readonly Watford _instance = new Watford();

            private Watford() { }

            public static Watford Instance
            {
                get
                {
                    return _instance;
                }
            }

            #endregion

            public override string FullName { get { return "Watford"; } }

            public override string ShortCode { get { return "WAT"; } }
        }

        [Serializable]
        public sealed class WestBrom : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly WestBrom _instance = new WestBrom();
   
            private WestBrom(){}

            public static WestBrom Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName { get { return "West Bromwich Albion"; } }
            public override List<string> AltNames { get { return new List<string> { "West Brom" }; } }
            
            public override string ShortCode{ get { return "WBA"; } }
        }

        [Serializable]
        public sealed class WestHam : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly WestHam _instance = new WestHam();
   
            private WestHam(){}

            public static WestHam Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "West Ham United"; } }
            public override List<string> AltNames { get { return new List<string> { "West Ham" }; } }
            public override string ShortCode { get { return "WHU"; } }
        }

        [Serializable]
        public sealed class Wigan : AbstractClub
        {
            #region Singleton Pattern 

            private static readonly Wigan _instance = new Wigan();
   
            private Wigan(){}

            public static Wigan Instance
            {
                get 
                {
                    return _instance; 
                }
            }

            #endregion

            public override string FullName  { get { return "Wigan Athletic"; } }
            public override List<string> AltNames { get { return new List<string> { "Wigan" }; } }
            
            public override string ShortCode { get { return "WIG"; } }
        }

        #endregion

        #region Private Methods

        private static IEnumerable<IClub> allClubs = new List<IClub> {
            Arsenal.Instance,
            AstonVilla.Instance,
            Bournemouth.Instance,
            Burnley.Instance,
            Cardiff.Instance,
            Chelsea.Instance,
            CrystalPalace.Instance,
            Everton.Instance,
            Fulham.Instance,
            Hull.Instance,
            Leicester.Instance,
            Liverpool.Instance,
            ManCity.Instance,
            ManUtd.Instance,
            Newcastle.Instance,
            Norwich.Instance,
            QPR.Instance,
            Reading.Instance,
            Southampton.Instance,
            Spurs.Instance,
            Stoke.Instance,
            Sunderland.Instance,
            Swansea.Instance,
            Watford.Instance,
            WestBrom.Instance,
            WestHam.Instance,
            Wigan.Instance
        };

        private static Dictionary<string, IClub> clubsByFullName = allClubs
            .GroupBy(x => x.FullName)
            .ToDictionary(grp => grp.Key, grp => grp.Single());

        private static Dictionary<string, IClub> clubsByShortCode = allClubs
            .GroupBy(x => x.ShortCode)
            .ToDictionary(grp => grp.Key, grp => grp.Single());

        private static Dictionary<string, IClub> clubsByAltName = allClubs
            .Where(club => club.AltNames != null)
            .SelectMany(club => club.AltNames.Select(altname => new { AltName = altname, Club = club })) 
            .GroupBy(tuple => tuple.AltName)
            .ToDictionary(grp => grp.Key, grp => grp.Select(tuple => tuple.Club).Single());

        #endregion

    }
}
