using Damien.UpgradeSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damien.UpgradeSystem.Interfaces
{
    internal interface IUpgradeController
    {
        void Initialize();
        bool HasUpgrade(string upgradeName);
        void GiveUpgrade(string upgradeName);
        void RemoveUpgrade(string upgradeName);
        List<ScriptableObjects.Upgrade> GetCurrentUpgrades();
        ScriptableObjects.Upgrade GetUpgrade(string upgradeName);
    }
}