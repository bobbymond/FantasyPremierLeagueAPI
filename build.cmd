@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\FantasyPremierLeagueApi.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log /nr:false

mkdir build

%nuget% pack "src\FantasyPremierLeagueApi.csproj" -IncludeReferencedProjects -verbosity detailed -o build -p Configuration="%config%"