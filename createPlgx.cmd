@echo on

set PATH_KEEPASS=C:\

set PATH_PLUGIN=%~dp0
rem set PATH_PLUGIN=%PATH_PLUGIN%CustomIconDashboarder

%PATH_KEEPASS%\keepass.exe --plgx-create %PATH_PLUGIN% ^
   --plgx-prereq-kp:2.0 ^
   --plgx-prereq-net:2.0

