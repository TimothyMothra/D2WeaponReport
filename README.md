# D2 Weapon Report [![.NET Build And Test](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/BuildAndTest.yml/badge.svg)](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/BuildAndTest.yml) [![Build and Deploy](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/main_d2weaponreport.yml/badge.svg)](https://github.com/TimothyMothra/D2WeaponReport/actions/workflows/main_d2weaponreport.yml)
www.d2weaponreport.com

https://twitter.com/D2WeaponReport

This repo is a hobby project and practicing the skills/tools/technologies I use in my day job. I'm unable to guarentee any quality service at this time.

Destiny 2 Weapon Report attempts to objectively grade weapons based on its metadata. The information provided should not be trusted for actual gameplay.

This codebase is sloppy because I'm working in short bursts of large rewrites. _"Perfect is the enemy of done."_ I define a theory and then start exploring raw data from the manifest while looking for interesting insights.

## Resources

- [Official Api Signup](https://www.bungie.net/en/Application/Create)
- [Official Bungie on Github](https://github.com/Bungie-net)
- [Official Api Documentation](https://bungie-net.github.io/multi/index.html)
- [Community Wiki](http://destinydevs.github.io/BungieNetPlatform/)
- [DestinySets ApiExplorer](https://data.destinysets.com/api)
- [Additional Links](https://www.reddit.com/r/DestinyTheGame/comments/aj4jzj/destiny_api_usage/)

## Contributing

[Journal](journal/)

### Prerequisits
- .NET v5 SDK (https://dotnet.microsoft.com/download/dotnet/5.0)

#### Initialize local environment.
To get started you must initialze the local environment. 
This will download the latest manifest db from Bungie.
Unit tests expect this file.

1. Run these command using the cli:
   ```
   dotnet run --project .\src\InitEnvironment\InitEnvironment.csproj
   ```

2. Now run this command to execute all tests. 
   This will confirm that the environment has been initialized.
   ```
   dotnet test .\src\Tests\Tests.csproj
   ```
