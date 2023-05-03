
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
    public class StatisticController : IStatisticController
    {
        private readonly UpgradeController _upgradeController;
        private readonly List<Statistic> _statistics;

        public Dictionary<Statistic, int> _debuffs = new Dictionary<Statistic, int>();
        public Dictionary<Statistic, int> _buffs = new Dictionary<Statistic, int>();

        public StatisticController(UpgradeController upgradeController)
        {
            _upgradeController = upgradeController;
        }

        public async Task Buff(Statistic statistic, int amount, int timeInMs)
        {
            _buffs.Add(statistic, amount);
            await Task.Delay(30000);
            _buffs.Remove(statistic);
        }

        public async Task Debuff(Statistic statistic, int amount, int timeInMs)
        {
            _buffs.Add(statistic, amount);
            await Task.Delay(30000);
            _buffs.Remove(statistic);
        }

        public int GetCurrentStatisticValue(string statisticName)
        {
            var sum = _upgradeController.GetUpgrades()
                .Where(upgrade => upgrade.Statistics.Any(statistic => statistic.Key.Name == statisticName))
                .SelectMany(upgrade => upgrade.Statistics).Sum(x => x.Value);
            return sum;
        }

        public Dictionary<Statistic, int> GetCurrentStatisticValues()
        {
            var statisticResults = _upgradeController.GetUpgrades()
               .SelectMany(obj => obj.Statistics)
               .GroupBy(statistic => statistic.Key.Name)
               .ToDictionary(group => GetStatistic(group.Key), group => group.Sum(statistic => statistic.Value)
               + _buffs.Where(x => x.Key.Name == group.Key).Sum(x => x.Value) - _debuffs.Where(x => x.Key.Name == group.Key).Sum(x => x.Value));
            return statisticResults;
        }

        public Statistic GetStatistic(string statisticName)
        {
            var statistic = _statistics.FirstOrDefault(statistic => statistic.Name == statisticName);
            if (statistic == null)
                throw new StatisticNotFoundException(statisticName, "Could not find a statistic matching the name that was provided.");
            return statistic;
        }

        public void Initialize()
        {
            var statistics = (Statistic[])Resources.FindObjectsOfTypeAll(typeof(Statistic));
            _statistics.ToList();
        }
    }

}