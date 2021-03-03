# Getting Started

## Tools
- [PostMan](https://www.postman.com/downloads/) for manually querying the API
- [SQLiteStudio](https://sqlitestudio.pl/) for browsing the sqlite manifest (.content) 

## Resources
- Community Destiny Api Discussion: https://discord.gg/rJYbzuJhsT

## Register for an Api-Key

- Go to https://www.bungie.net/en/Application/Create and create a new app. Call it "<name>DestinySandbox". Don't worry about these details, this can be changed as needed.
- This site will give you a GUID, that is your api-key.
- Using Postman, you can now make queries.
    - IMPORTANT: PAY ATTENTION TO THE URL; "Destiny" VS "Destiny2"!
    - GET: https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/1274330687/
    - Headers: Key `x-api-key` Value `<GUID API KEY WITHOUT HYPHENS>`
 
 ## Manifests
 The manifest is a sqlite database with static values.
  - https://destinydevs.github.io/BungieNetPlatform/docs/Manifest
  - How to Read the Manifest
https://www.bungie.net/sr/Groups/Post?groupId=39966&postId=105901734&sort=0&path=0&showBanned=0
- https://github.com/vpzed/Destiny2-API-Info/wiki/API-Introduction-Part-3-Manifest
- https://github.com/vpzed/Destiny2-API-Info/wiki/API-Introduction-Part-5-Item-Examples
 
 ### How to Download
- Using api-key, query GET: https://www.bungie.net/platform/Destiny2/Manifest 
    - IMPORTANT: Pay attention to the path "Destiny" vs "Destiny2". 
- This will return a json of several manifest content files.
    - For example: https://www.bungie.net/common/destiny2_content/sqlite/en/world_sql_content_30e996ad1e317a77a5130f587198da50.content (this link works without an api-key)
- This .content file is zipped. First change the extension to .zip and unzip. Inside there is another .content file, which is the actual database.
- Open this inner .content file using SQLiteStudio to explore meta data.

## Reading the database

While querying, get familiar with the `DestinyInventoryItemDefinition` table.
https://bungie-net.github.io/multi/schema_Destiny-Definitions-DestinyInventoryItemDefinition.html

Can lookup any item's json in either https://www.light.gg/ or https://data.destinysets.com/



### QUERYING for a list of all weapons
- Open the `world_sql_content` database.
- Query:
```sql
SELECT 
    id,
    json,
    json_extract(json, '$.collectibleHash') as collectibleHash,
    json_extract(json, '$.displayProperties.name') as name,
    json_extract(json, '$.itemType') as itemType
  FROM DestinyInventoryItemDefinition
  WHERE itemType = 3 -- enum DestinyItemType "WEAPON"
```


### TODO: HOW TO QUERY AN EXOTIC (With or Without Random Rolls)

There are currently two Exotics available with random rolls. How to discover these programatically?


### Querying for a single weapon (example: "Gnawing Hunger")
- Id: **821154603**
- https://data.destinysets.com/i/InventoryItem:821154603
- https://www.light.gg/db/items/821154603/gnawing-hunger/
- https://www.bungie.net/common/destiny2_content/screenshots/821154603.jpg
- Open the `world_sql_content` database. Sqlite Query:
```sql
SELECT 
    id,
    json,
    json_extract(json, '$.collectibleHash') as collectibleHash,
    json_extract(json, '$.displayProperties.name') as name,
    json_extract(json, '$.itemType') as itemType
  FROM DestinyInventoryItemDefinition
  WHERE 
  id = 821154603 
--name = 'Gnawing Hunger'
--name LIKE '%Gn%';
```

#### Other fields
Note: there is as $.quality.versions field. I haven't seen anything reference this yet, but it should be noted because Perks can change per version.

- $.equippingBlock.ammoType:1 // enum DestinyAmmunitionType "Primary"
- $.equippingBlock.equipmentSlotTypeHash
- $.inventory.tierTypeName:"Legendary", 
- $.summaryItemHash:3520001075 // <InventoryItem "Legendary Gear">
- $.stats (Example: Stability, Handling, Range, Aim Assistance, Attack, Inventory Size, Power, Recoil Direction, Zoom, Magazine, Impact, Reload Speed, Rounds Per Minute)
    - contains: hash, value, minumum, maximum, display maximum
- $.investmentStats -- assuming this is related to mods?
- $.perks.itemCategoryHashes:[] 3 items
    - 0:3 // <ItemCategory "Energy Weapon">
    - 1:1 // <ItemCategory "Weapon">
    - 2:5 // <ItemCategory "Auto Rifle">
- $.itemType:3 // enum DestinyItemType "Weapon"
- $.itemSubType:6 // enum DestinyItemSubType "AutoRifle"
- damageTypeHashes:[] 1 item
    - 0:3454344768 // <DamageType "Void">
- damageTypes:[] 1 item
    - 0:4 // enum DamageType "Void"
- $.defaultDamageType:4 // enum DamageType "Void"
- $.defaultDamageTypeHash:3454344768 // <DamageType "Void">
- $.sockets.socketEntries
    - 0:socketTypeHash:3956125808 // <SocketType> - <SocketCategory "INTRINSIC TRAITS">
    - 0:singleInitialItemHash:1294026524 // <InventoryItem "Adaptive Frame">

Kinetic?


### HOW TO QUERY AN ITEM'S POSSIBLE PERKS

- https://www.light.gg/db/items/821154603/gnawing-hunger/
- https://data.destinysets.com/i/InventoryItem:821154603


I recommend browsing perks with data.destysets.com because this will resolve some of the display strings.
Collections of perks are stored in `DestinyPlugSetDefinition`.

- From any weapon, locate the sockets.sockets > socketEntries > [n] > randomizedPlugSetHash. 
    - Shows that the 4th calumn can have: Rampage, Kill Clip, Multikill Clip, Demolitionist. https://data.destinysets.com/i/InventoryItem:821154603/PlugSet:1853656119
    - Can click on an individual perk to see it's details: Example, Kill Clip: https://data.destinysets.com/i/InventoryItem:821154603/InventoryItem:1015611457










### TODO: IS IT POSSIBLE TO QUERY FOR D2 WEAPONS FROM THE REST API?


### HOW TO QUERY ALL AVAILABLE PERKS (ex: rampage, outlaw)

DestinyInventoryItemDefinition > DestinyPlugSetDefinition > DestinyInventoryItemDefinition.investmentStats

Credit @abarrach 07/17/2018
> Lets take Crimson, for instance. 
> The base investment value (without any perk effects are added) for its Range stat is 55.
> It has two perks already activated that boost the Range stat (Smallbore & Accruzied Rounds) and they provide a total of +17 stat bonus. These values can be found in the perk item definition.
> So you add them up; 55 + 17 = 72.
> This value is the one before any interpolation is made. 
> You check the interpolation rules for this specific stat under the statGroupHash of the Crimson. 
> There we see the interpolation points for Range stat which are (0, 10) and (100,100)
> Here is the interpolation formula:

> You put the values in y is the final value we want to find. 
> y - 10 = ((100-10)/(100-0))*(72-0) -> y = 75 which is the final and correct value.

More reading:
https://github.com/Bungie-net/api/issues/797

### HOW TO QUERY ALL INTRINSIC PERKS 

```sql
SELECT id,
       json,
       json_extract(json, '$.displayProperties.name') as name,
       json_extract(json, '$.displayProperties.description') as description
  FROM DestinySandboxPerkDefinition;
```




- DestinySocketTypeDefinition
    - Weapon Perks: https://data.destinysets.com/i/InventoryItem:821154603/SocketType:3362409147
