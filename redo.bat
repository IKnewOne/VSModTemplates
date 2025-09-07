@echo off
rd /s /q bin
rd /s /q obj

dotnet pack
dotnet new uninstall KnewOne.VintageStory.Mod.Templates
dotnet new install .\bin\Release\KnewOne.VintageStory.Mod.Templates*.nupkg

echo %0 | findstr /I "^" >nul
if "%1"=="" pause