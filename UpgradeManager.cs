using Damien.UpgradeSystem.Exceptions;
using Damien.UpgradeSystem.Interfaces;
using Damien.UpgradeSystem.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Damien.UpgradeSystem
{
    public class UpgradeManager : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private UpgradeController _upgradeController;
        [SerializeField]
        private StatisticController _statisticController;
        #endregion

        /// <summary>
        /// This is invoked when a upgrade is added or changed.
        /// </summary>
        public UnityEvent OnUpgradeChange;

        /// <summary>
        /// This is invoked when a upgrade is added.
        /// </summary>
        public UnityEvent<string> OnUpgradeAdded;

        /// <summary>
        /// This is invoked when a upgrade is removed.
        /// </summary>
        public UnityEvent<string> OnUpgradeRemoved;

        /// <summary>
        /// This is invoked when a buff is added.
        /// </summary>
        public UnityEvent<string, int, int> OnBuffAdded;

        /// <summary>
        /// This is invoked when a buff is no longer in effect.
        /// </summary>
        public UnityEvent<string, int, int> OnBuffRemoved;

        /// <summary>
        /// This is invoked when a buff is added.
        /// </summary>
        public UnityEvent<string, int, int> OnDebuffAdded;

        /// <summary>
        /// This is invoked when a debuff is no longer in effect.
        /// </summary>
        public UnityEvent<string, int, int> OnDebuffRemoved;

        /// <summary>
        /// Saves the upgrade data to the persistent data path as UpgradeSystemSave.data.
        /// </summary>
        public void Save()
        {
            string saveContent = JsonUtility.ToJson(_upgradeController.GetCurrentUpgrades());
            System.IO.File.WriteAllText(Application.persistentDataPath + "/UpgradeSystemSave.data", saveContent);
        }


        /// <summary>
        /// This retrieves a list of all of the upgrades currently loaded into the upgrade manager.
        /// </summary>
        /// <returns>List of Upgrades</returns>
        public List<Upgrade> GetAllUpgrades()
        {
            return _upgradeController.GetAllUpgrades();
        }

        /// <summary>
        /// This retrieves a list of all the upgrades currently in use by the upgrade manager.
        /// </summary>
        /// <returns></returns>
        public List<Upgrade> GetCurrentUpgrades()
        {
            return _upgradeController.GetCurrentUpgrades();
        }

        /// <summary>
        /// Adds a upgrade to the list of upgrades that are currently in use.
        /// </summary>
        /// <param name="upgradeName">The name of the upgrade.</param>
        public void GiveUpgrade(string upgradeName)
        {
            _upgradeController.GiveUpgrade(upgradeName);
            OnUpgradeChange.Invoke();
            OnUpgradeAdded.Invoke(upgradeName);
        }

        /// <summary>
        /// Removes a upgrade to the list of upgrades that are currently in use.
        /// </summary>
        /// <param name="upgradeName">The name of the upgrade.</param>
        public void RemoveUpgrade(string upgradeName)
        {
            _upgradeController.RemoveUpgrade(upgradeName);
            OnUpgradeChange.Invoke();
            OnUpgradeRemoved.Invoke(upgradeName); 
        }

        /// <summary>
        /// Gets all of the statistics based on the upgrades currently in use by the upgrade manager.
        /// </summary>
        /// <returns>Dictionary where the key is the statistic and the value is the total according to the upgrades currently in use by the upgrade manager.</returns>
        public Dictionary<Statistic,int> GetStatistics()
        {
            return _statisticController.GetCurrentStatisticValues() ;
        }

        /// <summary>
        /// Gets the value of the give statistic based on the upgrades currently in use by the upgrade manager.
        /// </summary>
        /// <returns>The value of the statistic.</returns>
        public int GetStatisticValue(string statisticName)
        {
           return _statisticController.GetCurrentStatisticValue(statisticName);
        }

        /// <summary>
        /// Temporary raises a statistic.
        /// </summary>
        /// <param name="statisticName">The name of the statistic.</param>
        /// <param name="amount">The amount to increase by.</param>
        /// <param name="timeInMs">The amount of time to stay increased in miliseconds</param>
        public async void Buff(string statisticName, int amount, int timeInMs)
        {
            OnBuffAdded.Invoke(statisticName, amount, timeInMs);
            await _statisticController.Buff(statisticName, amount, timeInMs);
            OnBuffRemoved.Invoke(statisticName, amount, timeInMs);
        }

        /// <summary>
        /// Temporary lowers a statistic. Do not await this.
        /// </summary>
        /// <param name="statisticName">The name of the statistic.</param>
        /// <param name="amount">The amount to increase by.</param>
        /// <param name="timeInMs">The amount of time to stay lowered in miliseconds</param>
        public async void Debuff(string statisticName, int amount, int timeInMs)
        {
            OnDebuffAdded.Invoke(statisticName, amount, timeInMs);
            await _statisticController.Debuff(statisticName, amount, timeInMs);
            OnDebuffRemoved.Invoke(statisticName, amount, timeInMs);
        }

        private void Start()
        {
            _upgradeController = new UpgradeController();
            _statisticController = new StatisticController(_upgradeController);
            _upgradeController.Initialize();
            _statisticController.Initialize();
        }
    }
}
