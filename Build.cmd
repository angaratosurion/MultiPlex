@echo off

%windir%\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .nuget\nuget.targets /t:RestorePackages

echo abc | powershell -Version 2.0 -NonInteractive -NoProfile -ExecutionPolicy unrestricted -Command "$psakeDir = ([array](dir %~dp0packages\psake.*))[-1]; .$psakeDir\tools\psake.ps1 .\BuildScript.ps1 -framework '3.5' -properties @{configuration='Release'} net35; if ($psake.build_success -eq $false) { exit 1 } else { exit 0 }"
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%
echo abc | powershell -Version 2.0 -NonInteractive -NoProfile -ExecutionPolicy unrestricted -Command "$psakeDir = ([array](dir %~dp0packages\psake.*))[-1]; .$psakeDir\tools\psake.ps1 .\BuildScript.ps1 -framework '4.0' -properties @{configuration='Release'} %*; if ($psake.build_success -eq $false) { exit 1 } else { exit 0 }"
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%