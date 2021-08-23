﻿namespace DestinyLib.Scenarios
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public static class SearchForWeaponScenario
    {
        private static IList<SearchableWeaponRecord> searchableWeapons;

        static SearchForWeaponScenario()
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var worldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);

            // init
            var searchableWeapons = worldSqlContentProvider.GetSearchableWeapons();

            foreach (var searchableWeapon in searchableWeapons)
            {
                if (searchableWeapon.CollectibleHash != default)
                {
                    var collectibleDefinition = worldSqlContentProvider.GetDestinyCollectibleDefinitions(searchableWeapon.CollectibleHash);
                    if (collectibleDefinition != null)
                    {
                        searchableWeapon.CollectionDefintitionIconPath = collectibleDefinition.IconPath;
                    }
                }
            }

            SearchForWeaponScenario.searchableWeapons = searchableWeapons;
        }

        public enum SearchType
        {
            StringContains,
            Regex,
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="searchType"></param>
        /// <remarks>
        /// (https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference).
        /// (https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-options).
        /// </remarks>
        /// <returns></returns>
        public static IList<SearchableWeaponRecord> Run(string pattern, SearchType searchType)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(nameof(pattern));
            }
            else if (pattern.Length > 256)
            {
                // TODO: SHOULD VALIDATE USER INPUT
                throw new ArgumentException("input is too large", nameof(pattern));
            }

            if (searchType == SearchType.StringContains)
            {
                return searchableWeapons.Where(x => x.Name.Contains(pattern, System.StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            else if (searchType == SearchType.Regex)
            {
                string wildcard = ".*";

                string regexPattern = wildcard;
                foreach (char c in pattern)
                {
                    regexPattern += c + wildcard;
                }

                var regex = new Regex(pattern: regexPattern, options: RegexOptions.IgnoreCase);

                return searchableWeapons.Where(x => regex.IsMatch(x.Name)).ToList();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
