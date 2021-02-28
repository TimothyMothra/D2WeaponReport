# How to query a weapon

## List of all weapons
TODO

## Single weapon
For any item <Name>, how to find it in the API?


### 
Can lookup any item's json in either https://www.light.gg/ or https://data.destinysets.com/


## Api
Destiny1:
https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/{}/
with Auth key in header (x-api-key)

Destiny2: TODO THIS

## "Gnawing Hunger" Example
Example: Gnawing Hunger
- Item Id appears to be **821154603**
- [data.destinysets](https://data.destinysets.com/i/InventoryItem:821154603)
- [light.gg](https://www.light.gg/db/items/821154603/gnawing-hunger/)
- https://www.bungie.net/common/destiny2_content/screenshots/821154603.jpg

- Sqlite Query:
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


### Examples:
- [Gjallarhorn](https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/1274330687/) - this doesn't appear to work for other item ids
- TODO: HOW TO QUERY AN EXOTIC
- TODO: HOW TO QUERY AN EXOTIC WITH RANDOM ROLLS
- TODO: HOW TO QUERY A LEGENDARY WITH RANDOM ROLLS

## MetaData
I think these values will be in the manifest files.

- TODO: HOW TO QUERY WEAPON TYPE (ex: auto rifle, smg)
- TODO: HOW TO QUERY WEAPON ELEMENT (ex: kinetic, solar, arc, void)
- TODO: HOW TO QUERY BASE STATS (ex: impact, range, stability)
- TODO: HOW TO QUERY ALL POSSIBLE PERKS (ex: rampage, outlaw)


DestinySets makes it easy to understand what these ids are
- Example: [Gnawing Hunger](https://data.destinysets.com/i/InventoryItem:821154603)
