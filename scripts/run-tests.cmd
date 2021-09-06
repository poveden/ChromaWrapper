@echo off

pushd %~dp0..

dotnet build -c Release --no-incremental /warnaserror
if %errorlevel% neq 0 exit /b %errorlevel%

rmdir /q /s test\TestResults

dotnet test -c Release --no-build --collect:"XPlat Code Coverage"
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet tool restore
dotnet tool update dotnet-reportgenerator-globaltool
dotnet reportgenerator -reports:test\TestResults\**\coverage.cobertura.xml -targetdir:coverage\report

popd
