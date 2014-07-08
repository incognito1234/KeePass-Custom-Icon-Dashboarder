@echo off

rem Uncomment this line if PATH_KEEPASS is not an environment variable
rem set PATH_KEEPASS=C:\

set SRC_PATH_PLUGIN=%~dp0

rem WARNING
rem this folder is removed at start and end of this command file
set TMP_PATH_PLUGIN=%TEMP%\CustomIconDashboarder

rmdir /s /q %TMP_PATH_PLUGIN%
mkdir %TMP_PATH_PLUGIN%

xcopy %SRC_PATH_PLUGIN%*.* %TMP_PATH_PLUGIN%
del %TMP_PATH_PLUGIN%\%~nx0

mkdir %TMP_PATH_PLUGIN%\Properties
xcopy /S %SRC_PATH_PLUGIN%Properties %TMP_PATH_PLUGIN%\Properties

mkdir %TMP_PATH_PLUGIN%\Lib
xcopy /S %SRC_PATH_PLUGIN%Lib %TMP_PATH_PLUGIN%\Lib

%PATH_KEEPASS%\keepass.exe --plgx-create %TMP_PATH_PLUGIN% ^
   --plgx-prereq-kp:2.0 ^
   --plgx-prereq-net:2.0

copy %TEMP%\CustomIconDashboarder.plgx %~dp0

echo Remove Temp files

rmdir /s /q %TMP_PATH_PLUGIN%
mkdir %TMP_PATH_PLUGIN%
rm %TEMP%\CustomIconDashboarder.plgx
   

