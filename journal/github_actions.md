# Setting up Github Actions



- set up yaml build (restore, compile, test). 
this defines run actions and build variables.
in effect, enables PR builds, but also runs when self-pushing to main.



- tests fails because missing local database.
Note that this repo requires the dev environment to be initialized. 

- yaml can also a run "dotnet run --project" cmd (see Readme).

- now tests pass

- private repos are allotted 2,000 minutes per month for actions.


- can use github caching to cache nuget dependencies
this feature is supported by dotnet.exe

Note that caching takes two or three runs to fully initialize the cache.
See log output for more information (Pre and Post step)

- caching takes only slightly less time than restoring dependencies.

- switching build from windows-latest to ubuntu-lates saves almost another minute!