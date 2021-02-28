# Getting Started

## Tools
- [PostMan](https://www.postman.com/downloads/) for manually querying the API
- [SQLiteStudio](https://sqlitestudio.pl/) for browsing the sqlite manifest (.content) 

## Register for an Api-Key

- Go to https://www.bungie.net/en/Application/Create and create a new app. Call it "<name>DestinySandbox". Don't worry about these details, this can be changed as needed.
- This site will give you a GUID, that is your api-key.
- Using Postman, you can now make queries.
    - GET: https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/1274330687/
    - Headers: Key `x-api-key` Value `<GUID API KEY WITHOUT HYPHENS>`
 
 ## Download manifests
 
- Using api-key, query GET: https://www.bungie.net/platform/Destiny2/Manifest 
    - IMPORTANT: Pay attention to the path "Destiny" vs "Destiny2". 
- This will return a json of several content files.
    - For example: https://www.bungie.net/common/destiny2_content/sqlite/en/world_sql_content_30e996ad1e317a77a5130f587198da50.content (this link works without an api-key)
- This .content file is zipped. First change the extension to .zip and unzip. Inside there is another .content file, which is the actual database.
- Open this inner .content file using SQLiteStudio to explore meta data.

