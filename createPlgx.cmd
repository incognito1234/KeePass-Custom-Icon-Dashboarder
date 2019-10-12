echo off

rem Uncomment this line if PATH_KEEPASS is not an environment variable
set PATH_KEEPASS=C:\Temp\KeePass-2.43

set SRC_PATH_PLUGIN=%~dp0

rem WARNING
rem this folder is removed at start and end of this command file
set TMP_PATH_PLUGIN=%TEMP%\CustomIconDashboarder

rmdir /s /q %TMP_PATH_PLUGIN%
mkdir %TMP_PATH_PLUGIN%

xcopy %SRC_PATH_PLUGIN%*.* %TMP_PATH_PLUGIN%
del %TMP_PATH_PLUGIN%\CustomIconDashboarder.csproj
copy %TMP_PATH_PLUGIN%\CustomIconDashboarder_plgx.csproj %TMP_PATH_PLUGIN%\CustomIconDashboarder.csproj
del %TMP_PATH_PLUGIN%\CustomIconDashboarder_plgx.csproj
del %TMP_PATH_PLUGIN%\%~nx0

mkdir %TMP_PATH_PLUGIN%\HtmlAgilityPack
xcopy /S %SRC_PATH_PLUGIN%HtmlAgilityPack %TMP_PATH_PLUGIN%\HtmlAgilityPack

mkdir %TMP_PATH_PLUGIN%\Properties
xcopy /S %SRC_PATH_PLUGIN%Properties %TMP_PATH_PLUGIN%\Properties

mkdir %TMP_PATH_PLUGIN%\LomsonLib
xcopy /S %SRC_PATH_PLUGIN%LomsonLib %TMP_PATH_PLUGIN%\LomsonLib
del %TMP_PATH_PLUGIN%\LomsonLib\LomsonLib.csproj
del %TMP_PATH_PLUGIN%\LomsonLib\Properties\AssemblyInfo.cs

%PATH_KEEPASS%\keepass.exe --plgx-create %TMP_PATH_PLUGIN% ^
   --plgx-prereq-kp:2.0 ^
   --plgx-prereq-net:4.5

copy %TEMP%\CustomIconDashboarder.plgx %~dp0
copy %~dp0\CustomIconDashboarder.plgx C:\temp\KeePass-2.43\Plugins

echo Remove Temp files

rmdir /s /q %TMP_PATH_PLUGIN%
   

