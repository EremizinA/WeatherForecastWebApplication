$currentDirectory = "WeatherForecast.Tests.Unit"
rm -r .\$currentDirectory\TestResults\ -Force
$coverageJson = Get-Content .\$currentDirectory\coverage.json -Raw | ConvertFrom-Json
$excludeAssemblies = $coverageJson.includeOrExcludeAssemblies -join ';'
$excludeClasses = $coverageJson.includeOrExcludeClasses -join ';'
dotnet test .\$currentDirectory\$currentDirectory.csproj --collect:"XPlat Code Coverage" --logger "console;verbosity=detailed"
reportgenerator -reports:.\$currentDirectory\TestResults\*\coverage.cobertura.xml -targetdir:.\$currentDirectory\coveragereport -reporttypes:Html -assemblyfilters:"$excludeAssemblies" -classfilters:"$excludeClasses"