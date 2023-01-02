<#
.SYNOPSIS 
    Build and generate coverate raport for tests

.DESCRIPTION
    To build and test, this script is required put reportgenerator and coverrage collector module 

.EXAMPLE
    Build_&_pack.ps1

.NOTES
    Version    : 1.0.3
    Author     : JanoPL
    Created on : 2023-01-02
    License    : MIT License
    Copyright  : (c) 2023 JanoPL
#>

dotnet restore
dotnet clean --configuration Release;
dotnet build --configuration Release;

dotnet test --no-build --no-restore --configuration Release -f net7.0 /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

if (Get-Command reportgenerator.exe -ErrorAction SilentlyContinue) {
    Write-Output "Generate Reports"
    reportgenerator.exe -reports:tests\*\coverage.net7.0.cobertura.xml -targetdir:coveragereport
} else {
    Write-Output "Report generator does'n exist, please install via 'dotnet tool install -g dotnet-reportgenerator-globaltool' "
}
