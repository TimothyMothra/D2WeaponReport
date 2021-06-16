SELECT 
    DestinyInventoryItemDefinition.id,
    json_extract(DestinyInventoryItemDefinition.json, '$.hash') as hash,
    json_extract(DestinyInventoryItemDefinition.json, '$.collectibleHash') as collectibleHash,
    json_extract(DestinyInventoryItemDefinition.json, '$.displayProperties.name') as name,
    json_extract(DestinyInventoryItemDefinition.json, '$.itemType') as itemType,
    json_extract(DestinyInventoryItemDefinition.json, '$.itemTypeDisplayName') as itemTypeDisplayName,
    json_extract(DestinyInventoryItemDefinition.json, '$.displayProperties.icon') as itemIcon,
    json_extract(DestinyCollectibleDefinition.json, '$.displayProperties.icon') as collectibleIcon

FROM DestinyInventoryItemDefinition
  
LEFT JOIN DestinyCollectibleDefinition ON 
    DestinyCollectibleDefinition.id = json_extract(DestinyInventoryItemDefinition.json, '$.collectibleHash')
    
WHERE 
    itemType = 3