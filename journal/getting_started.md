# Getting Started

## Tools
- [PostMan](https://www.postman.com/downloads/) for manually querying the API
- [SQLiteStudio](https://sqlitestudio.pl/) for browsing the sqlite manifest (.content) 

## Register for an Api-Key

- Go to https://www.bungie.net/en/Application/Create and create a new app. Call it "<name>DestinySandbox". Don't worry about these details, this can be changed as needed.
- This site will give you a GUID, that is your api-key.
- Using Postman, you can now make queries.
    - IMPORTANT: PAY ATTENTION TO THE URL; "Destiny" VS "Destiny2"!
    - GET: https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/1274330687/
    - Headers: Key `x-api-key` Value `<GUID API KEY WITHOUT HYPHENS>`
 
 ## Download manifests
 
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

### Querying for a single weapon (example: "Gnawing Hunger")
- Id: **821154603**
- https://data.destinysets.com/i/InventoryItem:821154603
- https://www.light.gg/db/items/821154603/gnawing-hunger/
- https://www.bungie.net/common/destiny2_content/screenshots/821154603.jpg

- Open the `world_sql_content` database. Sqlite Query:
    ```
    SELECT 
    id,
    json,
    json_extract(json, '$.collectibleHash') as collectibleHash,
    json_extract(json, '$.displayProperties.name') as name
  FROM DestinyInventoryItemDefinition
  WHERE 
  id = 821154603 
  or
  name = 'Gnawing Hunger'
  --name LIKE '%Gn%'
  ```





### QUERYING for a list of all weapons
- Open the `world_sql_content` database.
- Query:
```
SELECT 
    id,
    json,
    json_extract(json, '$.collectibleHash') as collectibleHash,
    json_extract(json, '$.displayProperties.name') as name,
    json_extract(json, '$.itemType') as itemType
  FROM DestinyInventoryItemDefinition
  WHERE itemType = 3 -- enum DestinyItemType "WEAPON"
```



### TODO: IS IT POSSIBLE TO QUERY FOR D2 WEAPONS FROM THE REST API?


### TODO: HOW TO QUERY AN EXOTIC
### TODO: HOW TO QUERY AN EXOTIC WITH RANDOM ROLLS
### TODO: HOW TO QUERY A LEGENDARY WITH RANDOM ROLLS

## MetaData
I think these values will be in the manifest files.

### TODO: HOW TO QUERY WEAPON TYPE (ex: auto rifle, smg)
### TODO: HOW TO QUERY WEAPON ELEMENT (ex: kinetic, solar, arc, void)
### TODO: HOW TO QUERY BASE STATS (ex: impact, range, stability)
### TODO: HOW TO QUERY ALL POSSIBLE PERKS (ex: rampage, outlaw)

