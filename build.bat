REM @echo Off
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

set msbuildPath=%MsBuildExe%
if msbuildPath == "" (
   set msbuildExe= "%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe""
)

REM Restore dependent packages
call %nuget% restore src\FantasyPremierLeagueApi.sln

REM Build
%msbuildExe% src\FantasyPremierLeagueApi.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir build
call %nuget% pack "src\FantasyPremierLeagueApi.csproj" -IncludeReferencedProjects -o build -p Configuration=%config% %version%