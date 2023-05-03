using Damien.UpgradeSystem.Exceptions;
using Damien.UpgradeSystem.Interfaces;
using Damien.UpgradeSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Damien.UpgradeSystem
{
    [System.Serializable]
    public class UpgradeController
    {

        [SerializeField]
        private List<ScriptableObjects.Upgrade> _upgrades;
        [SerializeField]
        private List<ScriptableObjects.Upgrade> _currentUpgrades = new List<ScriptableObjects.Upgrade>();
        public UpgradeController()
        {
        }

        public void GiveUpgrade(string upgradeName)
        {
            var upgrade = InternalGetUpgrade(upgradeName);
            _currentUpgrades.Add(upgrade);
        }

        public bool HasUpgrade(string upgradeName)
        {
            return _currentUpgrades.Any(x => x.Name == upgradeName);
        }

        public void Initialize()
        {
            var upgrades = (ScriptableObjects.Upgrade[])Resources.FindObjectsOfTypeAll(typeof(ScriptableObjects.Upgrade));
            _upgrades = upgrades.ToList();
        }

        public void RemoveUpgrade(string upgradeName)
        {
            var upgrade = InternalGetUpgrade(upgradeName);
            var playerUpgrade = _currentUpgrades.FirstOrDefault(x => x.Name == upgrade.Name);
            if (playerUpgrade == null)
                throw new UpgradeNotFoundException(upgradeName, "Could not remove upgrade because it is not in the current upgrades list.");
            _currentUpgrades.Add(upgrade);
        }

        public Upgrade GetUpgrade(string upgradeName)
        {
            var upgrade = _upgrades.FirstOrDefault(upgrade => upgrade.Name == upgradeName);
            if (upgrade == null)
                throw new UpgradeNotFoundException(upgradeName, "Could not find a upgrade matching the name that was provided.");
            return upgrade.ToModel();
        }
        internal ScriptableObjects.Upgrade InternalGetUpgrade(string upgradeName)
        {
            var upgrade = _upgrades.FirstOrDefault(upgrade => upgrade.Name == upgradeName);
            if (upgrade == null)
                throw new UpgradeNotFoundException(upgradeName, "Could not find a upgrade matching the name that was provided.");
            return upgrade;
        }

        public List<Upgrade> GetCurrentUpgrades()
        {
            return _currentUpgrades.ToModel();
        }

        public List<Upgrade> GetAllUpgrades()
        {
            return _upgrades.ToModel();
        }
    }

}