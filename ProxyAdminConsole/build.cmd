@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ProxyAdminConsole.csproj /p:Configuration="%config%";ExcludeXmlAssemblyFiles=false /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false /m

nuget pack -OutputDirectory artifacts\Release\