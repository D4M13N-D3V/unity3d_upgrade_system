
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
    internal class StatisticController
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
                .SelectMany(upgrade => upgrade.Statistics).Sum(x => x.Amount);
            return sum;
        }

        internal Dictionary<ScriptableObjects.Statistic, int> GetCurrentStatisticValues()
        {
            var upgrades = _upgradeController.GetCurrentUpgrades();
            var statisticResults = upgrades
    .SelectMany(obj => obj.Statistics)
    .GroupBy(statistic => statistic.Name)
    .ToDictionary(
        group => GetStatistic(group.Key),
        group => group.Sum(statistic => statistic.Amount)
                 + _buffs.Where(x => x.Key == group.Key).Sum(x => x.Value ?? 0)
                 - _debuffs.Where(x => x.Key == group.Key).Sum(x => x.Value ?? 0)
    );

            return statisticResults;
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