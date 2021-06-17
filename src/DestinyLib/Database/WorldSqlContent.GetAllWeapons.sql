SELECT 
    id,
    json_extract(json, '$.hash') as hash,
    json_extract(json, '$.collectibleHash') as collectibleHash,
    json_extract(json, '$.displayProperties.name') as name,
    json_extract(json, '$.displayProperties.icon') as icon,
    json_extract(json, '$.itemType') as itemType,
    json_extract(json, '$.itemTypeDisplayName') as itemTypeDisplayName
  FROM DestinyInventoryItemDefinition
  WHERE 
  itemType = 3