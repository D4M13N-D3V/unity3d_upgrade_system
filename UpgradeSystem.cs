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
    public class UpgradeSystem : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private UpgradeController _upgradeController;
        [SerializeField]
        private StatisticController _statisticController;
        #endregion

        public UnityEvent OnUpgradeChange;
        public UnityEvent<string> OnUpgradeAdded;
        public UnityEvent<string> OnUpgradeRemoved;
        public UnityEvent<string, int, int> OnBuffAdded;
        public UnityEvent<string, int, int> OnBuffRemoved;
        public UnityEvent<string, int, int> OnDebuffAdded;
        public UnityEvent<string, int, int> OnDebuffRemoved;

        public void Save()
        {
            string saveContent = JsonUtility.ToJson(_upgradeController.GetCurrentUpgrades());
            System.IO.File.WriteAllText(Application.persistentDataPath + "/UpgradeSystemSave.data", saveContent);
        }

        public List<Upgrade> GetAllUpgrades()
        {
            return _upgradeController.GetAllUpgrades();
        }

        public List<Upgrade> GetCurrentUpgrades()
        {
            return _upgradeController.GetCurrentUpgrades();
        }

        public void GiveUpgrade(string upgradeName)
        {
            _upgradeController.GiveUpgrade(upgradeName);
            OnUpgradeChange.Invoke();
            OnUpgradeAdded.Invoke(upgradeName);
        }

        public void RemoveUpgrade(string upgradeName)
        {
            _upgradeController.RemoveUpgrade(upgradeName);
            OnUpgradeChange.Invoke();
            OnUpgradeRemoved.Invoke(upgradeName); 
        }

        public Dictionary<Statistic,int> GetStatistics()
        {
            return _statisticController.GetCurrentStatisticValues() ;
        }

        public int GetStatisticValue(string statisticName)
        {
           return _statisticController.GetCurrentStatisticValue(statisticName);
        }

        public async void Buff(string statisticName, int amount, int timeInMs)
        {
            OnBuffAdded.Invoke(statisticName, amount, timeInMs);
            await _statisticController.Buff(statisticName, amount, timeInMs);
            OnBuffRemoved.Invoke(statisticName, amount, timeInMs);
        }

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
