set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

set msbuildPath="%MsBuildExe%"
if %msbuildPath% == "" (
   set msbuildPath= "%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild"
)

REM Restore dependent packages
call %nuget% restore src\FantasyPremierLeagueApi.sln

REM Build
%msbuildPath% src\FantasyPremierLeagueApi.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir build
call %nuget% pack "src\Api.csproj" -IncludeReferencedProjects -o build -p Configuration=%config% %version%