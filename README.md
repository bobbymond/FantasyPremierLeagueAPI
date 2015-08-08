FantasyPremierLeagueAPI
=======================
Copyright (c) 2013,2014,2015 Mark Hammond.  See license.txt for license.

Overview
--------
FantasyPremierLeague API is an unofficial c# library allowing convenvient access to data from the popular Fantasy Premier League game (fantasy.premierleague.com).
It is intended for use by people wishing to write algorithms to pick their team programmatically.

Current methods supported:
 * GetGameweekNumber - returns int
 * GetClubSeasonPerformances - returns a dictionary mapping clubs to their home & away performance in the league so far
 * GetAllPlayers - returns all stats for this season per player
 * GetMySquad - returns a set of 15 player stat objects representing your currently selected team
 * GetRemainingBudget - returns the amount of money currently available for transfers
 * GetNextWeekStartingElevenByTeam - returns a dictionary mapping clubs to their expected starting XI players, as scraped from www.fantasyfootballscout.co.uk

How to Use
----------
Pre-requisites
 * You will need a username and password for the game site to use any of the methods
 * You need to install NuGet for 3rd party packages: http://nuget.codeplex.com/
 * In Visual Studio, you should enable "Allow NuGet to download missing packages during build". See http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages

Api Methods
 * All API methods are accessed via FantasyPremierLeagueApi.Api.FantasyPremierLeagueApi or FantasyPremierLeagueApi.Api.FantasyScoutApi
 * The API constructor requires username and password and optional ILogger object (defaults to outputting to console)
 * All API methods are public members of FantasyPremierLeagueApi.Api.FantasyPremierLeagueApi
 * The FantasyPremierLeagueApi.Model namespace/project contains all model classes

Getting Started/Test Methods
 * To try the API methods, try the tests in FantasyPremierLeagueApi.Api.Test (these are MStests)
 * You will need to configure your username/password in FantasyPremierLeagueApi.Api.Test\TestConstants.cs