using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Damien.UpgradeSystem.ScriptableObjects
{
    public abstract class UpgradeSystemEntity:ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public List<Requirment> Requirments = new List<Requirment>();
        public bool MeetsRequirments(List<Upgrade> upgrades, Dictionary<Statistic, int> stats)
        {
            var hasUpgrades = CheckUpgrades(upgrades);
            var hasStats = CheckStats(stats);
            return hasUpgrades && hasStats;
        }

        private bool CheckUpgrades(List<Upgrade> upgrades)
        {
            var hasUpgrades = false;
            var upgradeRequirments = GetUpgradeRequirments();
            foreach (var requirment in upgradeRequirments)
            {
                hasUpgrades = upgrades.Any(entity => entity.name == requirment.Upgrade.Name);
                if(hasUpgrades==false)
                    break;
            }
            return hasUpgrades;
        }
        private bool CheckStats(Dictionary<Statistic,int> stats)
        {
            var hasStats = false;
            var upgradeRequirments = GetStatisticRequirments();
            foreach (var requirment in upgradeRequirments)
            {
                if (stats.Any(entity => entity.Key.name == requirment.Statistic.name)==false)
                {
                    hasStats = false;
                    break;
                }

                hasStats = stats.FirstOrDefault(entity => entity.Key.name == requirment.Statistic.name).Value>=requirment.Minimum && stats.FirstOrDefault(entity => entity.Key.name == requirment.Statistic.name).Value >= requirment.Minimum;
                if(hasStats==false)
                    break;
            }
            return hasStats;
        }

        private List<UpgradeRequirment> GetUpgradeRequirments()
        {
            return Requirments.Where(requirment => requirment is UpgradeRequirment)
                .Select(requirment => requirment as UpgradeRequirment).ToList();
        }

        private List<StatisticRequirment> GetStatisticRequirments()
        {
            return Requirments.Where(requirment => requirment is StatisticRequirment)
                .Select(requirment => requirment as StatisticRequirment).ToList();
        }
    }
}