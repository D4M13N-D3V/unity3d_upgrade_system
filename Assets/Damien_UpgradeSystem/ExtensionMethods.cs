using Damien.UpgradeSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Damien.UpgradeSystem
{
    internal static class ExtensionMethods
    {
        public static Upgrade ToModel(this ScriptableObjects.Upgrade upgrade)
        {
            return new Upgrade(upgrade.Name, upgrade.Description, upgrade.Icon, upgrade.Statistics.ToModel());
        }
        public static List<Upgrade> ToModel(this List<ScriptableObjects.Upgrade> upgrades)
        {
            return upgrades.Select(upgrade => new Upgrade(upgrade.Name, upgrade.Description, upgrade.Icon, upgrade.Statistics.ToModel())).ToList();
        }
        public static Dictionary<Statistic, int> ToModel(this Dictionary<ScriptableObjects.Statistic,int> statistics)
        {
            var result = new Dictionary<Statistic, int>(statistics.Count());
            foreach (var kvp in statistics)
            {
                result.Add(kvp.Key.ToModel(), kvp.Value);
            }
            return result;
        }
        public static List<Statistic> ToModel(this List<ScriptableObjects.UpgradeStatistic> statistics)
        {
            return statistics.Select(statistic => new Statistic()
            {
                Name = statistic.Statistic.Name,
                Amount = statistic.Amount,
                Description = statistic.Statistic.Description,
                Icon = statistic.Statistic.Icon
            }).ToList();
        }
        public static Statistic ToModel(this ScriptableObjects.UpgradeStatistic statistic)
        {
            return new Statistic()
            {
                Name = statistic.Statistic.Name,
                Amount = statistic.Amount,
                Description = statistic.Statistic.Description,
                Icon = statistic.Statistic.Icon
            };
        }
        public static Statistic ToModel(this ScriptableObjects.Statistic statistic)
        {
            return new Statistic()
            {
                Name = statistic.Name,
                Amount = null,
                Description = statistic.Description,
                Icon = statistic.Icon
            };
        }
    }
}