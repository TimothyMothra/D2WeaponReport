namespace DestinyLib.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DestinyLib.Database;

    public class AnalysisController
    {
        public readonly WorldSqlContent WorldSqlContent;

        public AnalysisController(WorldSqlContent worldSqlContent)
        {
            this.WorldSqlContent = worldSqlContent;
        }

        public WeaponDefinition GetWeaponDefinition(int id)
        {
            var record = this.WorldSqlContent.GetWeaponItemDefinition(id);


            var weapon = new WeaponDefinition
            {
                MetaData = new WeaponDefinition.WeaponMetaData
                { 
                    Name = record.Name,
                    AmmoTypeName = record.AmmoType.ToString(), //TODO: THIS
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };



            return weapon;
        }

    }
}
