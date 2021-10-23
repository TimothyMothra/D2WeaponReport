
# D2 Weapon Report

[![.NET Build And Test](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/BuildAndTest.yml/badge.svg)](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/BuildAndTest.yml)

[![Build and Deploy](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/main_SandboxWeb20210922151420.yml/badge.svg)](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/main_SandboxWeb20210922151420.yml)

## Resources

- [Official Api Signup](https://www.bungie.net/en/Application/Create)
- [Official Bungie on Github](https://github.com/Bungie-net)
- [Official Api Documentation](https://bungie-net.github.io/multi/index.html)
- [Community Wiki](http://destinydevs.github.io/BungieNetPlatform/)
- [DestinySets ApiExplorer](https://data.destinysets.com/api)
- [Additional Links](https://www.reddit.com/r/DestinyTheGame/comments/aj4jzj/destiny_api_usage/)

## Contributing

Please view the journal  [here](journal/).


### Prerequisits
- .NET v5 SDK (https://dotnet.microsoft.com/download/dotnet/5.0)

### Initialize local environment.
To get started you must initialze the local environment. 
This will download the latest manifest db from Bungie.
Unit tests expect this file.

Run these command using the windows console:
```
dotnet run --project .\src\InitEnvironment\InitEnvironment.csproj
```

Now run this command to execute all tests:
```
dotnet test .\src\Tests\Tests.csproj
```
