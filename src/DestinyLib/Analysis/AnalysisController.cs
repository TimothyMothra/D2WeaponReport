namespace DestinyLib.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public class AnalysisController
    {
        public readonly WorldSqlContent WorldSqlContent;
        public readonly DataController DataController;

        private readonly IList<SearchableWeapon> searchableWeapons;

        public AnalysisController(WorldSqlContent worldSqlContent)
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
        public IList<SearchableWeapon> Search(string pattern, SearchType searchType)
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
                foreach(char c in pattern)
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

        //public WeaponDefinition GetWeaponDefinition(int id)
        //{
        //    var record = this.WorldSqlContent.GetWeaponItemDefinition(id);


        //    var weapon = new WeaponDefinition
        //    {
        //        MetaData = new WeaponDefinition.WeaponMetaData
        //        { 
        //            Name = record.Name,
        //            AmmoTypeName = record.AmmoType.ToString(), //TODO: THIS
        //        },
        //        Stats = new List<WeaponDefinition.WeaponStat>(),
        //        PerkSets = new List<WeaponDefinition.PerkSet>(),
        //    };



        //    return weapon;
        //}

        public enum SearchType
        {
            StringContains,
            Regex
        }
    }
}
