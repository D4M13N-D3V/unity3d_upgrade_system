using Damien.UpgradeSystem.Exceptions;
using Damien.UpgradeSystem.Interfaces;
using Damien.UpgradeSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Damien.UpgradeSystem
{
    public class UpgradeController : IUpgradeController
    {
        private readonly List<Upgrade> _upgrades;
        private readonly List<Upgrade> _currentUpgrades = new List<Upgrade>();
        public UpgradeController()
        {
            Initialize();
        }

        public void GiveUpgrade(string upgradeName)
        {
            var upgrade = GetUpgrade(upgradeName);
            _currentUpgrades.Remove(upgrade);
        }

        public bool HasUpgrade(string upgradeName)
        {
            return _currentUpgrades.Any(x => x.Name == upgradeName);
        }

        public void Initialize()
        {
            var upgrades = (Upgrade[])Resources.FindObjectsOfTypeAll(typeof(Upgrade));
            _upgrades.ToList();
        }

        public void RemoveUpgrade(string upgradeName)
        {
            var upgrade = GetUpgrade(upgradeName);
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
            return upgrade;
        }

        public List<Upgrade> GetUpgrades()
        {
            return _currentUpgrades;
        }
    }

}