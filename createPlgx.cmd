@echo off

rem Uncomment this line if PATH_KEEPASS is not an environment variable
set PATH_KEEPASS=C:\workspace\KeePass\KeePass-2.30

set SRC_PATH_PLUGIN=%~dp0

rem WARNING
rem this folder is removed at start and end of this command file
set TMP_PATH_PLUGIN=%TEMP%\CustomIconDashboarder

rmdir /s /q %TMP_PATH_PLUGIN%
mkdir %TMP_PATH_PLUGIN%

xcopy %SRC_PATH_PLUGIN%*.* %TMP_PATH_PLUGIN%
del %TMP_PATH_PLUGIN%\%~nx0

mkdir %TMP_PATH_PLUGIN%\HtmlAgilityPack
xcopy /S %SRC_PATH_PLUGIN%HtmlAgilityPack %TMP_PATH_PLUGIN%\HtmlAgilityPack

mkdir %TMP_PATH_PLUGIN%\Properties
xcopy /S %SRC_PATH_PLUGIN%Properties %TMP_PATH_PLUGIN%\Properties

mkdir %TMP_PATH_PLUGIN%\Lib
xcopy /S %SRC_PATH_PLUGIN%Lib %TMP_PATH_PLUGIN%\Lib

%PATH_KEEPASS%\keepass.exe --plgx-create %TMP_PATH_PLUGIN% ^
   --plgx-prereq-kp:2.0 ^
   --plgx-prereq-net:2.0

copy %TEMP%\CustomIconDashboarder.plgx %~dp0
copy %~dp0\CustomIconDashboarder.plgx C:\workspace\KeePass\KeePass-2.25\Plugins
copy %~dp0\CustomIconDashboarder.plgx C:\workspace\KeePass\KeePass-2.26\Plugins
copy %~dp0\CustomIconDashboarder.plgx C:\workspace\KeePass\KeePass-2.27\Plugins
copy %~dp0\CustomIconDashboarder.plgx C:\workspace\KeePass\KeePass-2.28\Plugins
copy %~dp0\CustomIconDashboarder.plgx C:\workspace\KeePass\KeePass-2.29\Plugins
copy %~dp0\CustomIconDashboarder.plgx C:\workspace\KeePass\KeePass-2.30\Plugins

echo Remove Temp files

rmdir /s /q %TMP_PATH_PLUGIN%
   

