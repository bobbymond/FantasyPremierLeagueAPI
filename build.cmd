set config=%1
if "%config%" == "" (
   set config=Release
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

if "%MsBuildExe%" == "" (
  set MsBuildExe="%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild"
)

%nuget% restore src\FantasyPremierLeagueApi.sln

%MsBuildExe% src\FantasyPremierLeagueApi.sln /p:Configuration="%config%" /m /v:M /nr:false

mkdir build

%nuget% pack "src\FantasyPremierLeagueApi.csproj" -IncludeReferencedProjects -verbosity detailed -o build -p Configuration="%config%"