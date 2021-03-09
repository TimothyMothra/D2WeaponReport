
# root:     "C:\TimothyMothra\root.marker"
# assembly: "C:\TimothyMothra\src\DestinyLib\bin\Debug\net5.0\DestinyLib.dll"



[string]$assemblyPath = 'C:\TimothyMothra\src\DestinyLib\bin\Debug\net5.0\DestinyLib.dll'
Add-Type -Path $assemblyPath

# [DestinyLib.Interop.InitializeEnvironment]$initializeEnvironment =  New-Object -TypeName  'DestinyLib.Interop.InitializeEnvironment'
$test = [DestinyLib.Interop.InitializeEnvironment]::Test();

Write-Host($test);