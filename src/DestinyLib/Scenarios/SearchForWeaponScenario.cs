namespace DestinyLib.Scenarios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public class SearchForWeaponScenario
    {
        public readonly WorldSqlContent WorldSqlContent; //TODO: WHY BOTH OF THESE?
        public readonly DataController DataController;

        private readonly IList<SearchableWeaponRecord> searchableWeapons;

        public SearchForWeaponScenario(WorldSqlContent worldSqlContent)
        {
            this.WorldSqlContent = worldSqlContent;
            this.DataController = new DataController(worldSqlContent);
            this.searchableWeapons = this.DataController.GetSearchableWeapons();
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
        public IList<SearchableWeaponRecord> Run(string pattern, SearchType searchType) //TODO: MOVE PARAMS TO PROPERTIES
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
                return this.searchableWeapons.Where(x => x.Name.Contains(pattern, System.StringComparison.InvariantCultureIgnoreCase)).ToList();
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

                return this.searchableWeapons.Where(x => regex.IsMatch(x.Name)).ToList();
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        public enum SearchType
        {
            StringContains,
            Regex
        }
    }
}
