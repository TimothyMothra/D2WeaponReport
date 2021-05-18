﻿namespace DestinyLib.Scenarios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public static class SearchForWeaponScenario
    {
        public enum SearchType
        {
            StringContains,
            Regex
        }

        private static IList<SearchableWeaponRecord> searchableWeapons;

        static SearchForWeaponScenario()
        {
            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));

            // TODO: THIS IS FINE FOR NOW. SCENARIOS CAN OWN THEIR OPTIONS.
            // IN THE FUTURE, TESTS WILL NEED TO SET THEIR OWN OPTIONS.
            // EX: THIS IS NEEDED TO TEST WITH AND WITHOUT CACHING.
            var WorldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);
            searchableWeapons = WorldSqlContentProvider.GetSearchableWeapons();
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
            else if (!pattern.All(Char.IsLetterOrDigit))
            {
                throw new ArgumentException("must contain only letters or numbers", nameof(pattern));
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
