# Architecture (WIP)

## Scenario > WorldContentProvider > WorldSqlContent > Manifest DB (world_sql_content)


### Providers 
- must have knowledge about the DatabaseStructure. 
- must know which tables to read and how to parse.
- have the option to cache records after being parsed. // TODO.



### Scenario: Getting Weapon Definition

- GetWeaponDefinitionScenario > WorldContentProvider.GetWeaponDefinition(id) > WorldSqlContent.GetDestinyInventoryitemDefinition()
  - WorldContentProvider.GetWeaponStatDefinition() > WorldSqlContent.GetDestinyStatDefinition()
  - WorldContentProvider.GetWeaponDefinitionPerks() > WorldSqlContent.GetDestinyPlugSetDefinition()
    - WorldContentProvider.GetWeaponDefinitionPerk() > WorldSqlContent.GetDestinyInventoryItemDefinition()

