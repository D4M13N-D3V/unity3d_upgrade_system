
using Damien.UpgradeSystem.Exceptions;
using Damien.UpgradeSystem.Interfaces;
using Damien.UpgradeSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Damien.UpgradeSystem
{
    [System.Serializable]
    public class StatisticController
    {
        private readonly UpgradeController _upgradeController;
        private List<ScriptableObjects.Statistic> _statistics;

        [SerializeField]
        internal Dictionary<string, int?> _debuffs = new Dictionary<string, int?>();
        [SerializeField]
        internal Dictionary<string, int?> _buffs = new Dictionary<string, int?>();

        internal StatisticController(UpgradeController upgradeController)
        {
            _upgradeController = upgradeController;
        }

        internal async Task Buff(string statisticName, int amount, int timeInMs)
        {
            GetStatistic(statisticName);
            _buffs.Add(statisticName, amount);
            await Task.Delay(30000);
            _buffs.Remove(statisticName);
        }

        internal async Task Debuff(string statisticName, int amount, int timeInMs)
        {
            GetStatistic(statisticName);
            _buffs.Add(statisticName, amount);
            await Task.Delay(30000);
            _buffs.Remove(statisticName);
        }

        internal int GetCurrentStatisticValue(string statisticName)
        {
            var sum = _upgradeController.GetCurrentUpgrades()
                .Where(upgrade => upgrade.Statistics.Any(statistic => statistic.Name == statisticName))
                .SelectMany(upgrade => upgrade.Statistics).Sum(x => x.Amount ?? 0);
            return sum;
        }

        internal Dictionary<Statistic, int> GetCurrentStatisticValues()
        {
            var test = _buffs.Where(x => x.Key == "Apple").Sum(x => x.Value ?? 0);
            Dictionary<string, int> results = new Dictionary<string, int>();

            var upgrades = _upgradeController.GetCurrentUpgradesInternal();
            foreach (var upgrade in upgrades)
            {
                foreach(var statistic in upgrade.Statistics)
                {
                    if (results.ContainsKey(statistic.Statistic.Name) == false)
                        results.Add(statistic.Statistic.Name, 0);
                    results[statistic.Statistic.Name] += statistic.Amount;
                }
            }
            var statisticResults = results.ToDictionary(result => GetStatistic(result.Key), result => result.Value);
            return statisticResults.ToModel();
        }

        internal ScriptableObjects.Statistic GetStatistic(string statisticName)
        {
            var statistic = _statistics.FirstOrDefault(statistic => statistic.Name == statisticName);
            if (statistic == null)
                throw new StatisticNotFoundException(statisticName, "Could not find a statistic matching the name that was provided.");
            return statistic;
        }


        internal void Initialize()
        {
            var statistics = (ScriptableObjects.Statistic[])Resources.FindObjectsOfTypeAll(typeof(ScriptableObjects.Statistic));
            _statistics = statistics.ToList();
        }
    }

}