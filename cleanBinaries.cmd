@echo off

rmdir /Q /S %~dp0\obj
rmdir /Q /S %~dp0\bin
mkdir obj
mkdir bin

rmdir /Q /S %~dp0\LomsonLib\obj
rmdir /Q /S %~dp0\LomsonLib\bin
mkdir LomsonLib\obj
mkdir LomsonLib\bin
