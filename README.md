# D2 Weapon Report 
https://www.d2weaponreport.com

This repo is a hobby project and practicing the skills/tools/technologies I use in my day job. I'm unable to guarentee any quality service at this time.
Destiny 2 Weapon Report attempts to objectively grade weapons based on any weapon's metadata. The information provided should not be trusted for actual gameplay.



[![.NET Build And Test](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/BuildAndTest.yml/badge.svg)](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/BuildAndTest.yml)

[![Build and Deploy](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/main_d2weaponreport.yml/badge.svg)](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/main_d2weaponreport.yml)

## Resources

- [Official Api Signup](https://www.bungie.net/en/Application/Create)
- [Official Bungie on Github](https://github.com/Bungie-net)
- [Official Api Documentation](https://bungie-net.github.io/multi/index.html)
- [Community Wiki](http://destinydevs.github.io/BungieNetPlatform/)
- [DestinySets ApiExplorer](https://data.destinysets.com/api)
- [Additional Links](https://www.reddit.com/r/DestinyTheGame/comments/aj4jzj/destiny_api_usage/)

## Contributing

[Journal](journal/).


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
