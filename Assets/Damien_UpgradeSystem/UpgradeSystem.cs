using Damien.UpgradeSystem.Exceptions;
using Damien.UpgradeSystem.Interfaces;
using Damien.UpgradeSystem.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Damien.UpgradeSystem
{
    public class UpgradeSystem : MonoBehaviour
    {

        #region Private Variables
        private UpgradeController _upgradeController;
        private StatisticController _statisticController;
        #endregion

        #region Configuration
        #endregion

        public void GiveUpgrade(string upgradeName)
        {
            _upgradeController.GiveUpgrade(upgradeName);
        }

        public void RemoveUpgrade(string upgradeName)
        {
            _upgradeController.RemoveUpgrade(upgradeName);
        }

        public void GetStatistics()
        {
            _statisticController.GetCurrentStatisticValues();
        }

        public void GetStatistic(string statisticName)
        {
            _statisticController.GetCurrentStatisticValue(statisticName);
        }

        public async void Buff(string statisticName, int amount, int timeInMs)
        {
            var statistic = _statisticController.GetStatistic(statisticName);
            await _statisticController.Buff(statistic, amount, timeInMs);
        }

        public async void Buff(Statistic statistic, int amount, int timeInMs)
        {
            await _statisticController.Buff(statistic, amount, timeInMs);
        }

        public async void Debuff(string statisticName, int amount, int timeInMs)
        {
            var statistic = _statisticController.GetStatistic(statisticName);
            await _statisticController.Debuff(statistic, amount, timeInMs);
        }

        public async void Debuff(Statistic statistic, int amount, int timeInMs)
        {
            await _statisticController.Debuff(statistic, amount, timeInMs);
        }

        private void Start()
        {
            _upgradeController = new UpgradeController();
            _statisticController = new StatisticController(_upgradeController);
        }
    }
}
