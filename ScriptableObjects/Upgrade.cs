using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damien.UpgradeSystem.ScriptableObjects
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Damien/Upgrade System/Create Upgrade")]
    internal class Upgrade : UpgradeSystemEntity
    {
        [SerializeField]
        public List<UpgradeStatistic> Statistics = new List<UpgradeStatistic>();
    }

    [System.Serializable]
    internal class UpgradeStatistic
    {
        public Statistic Statistic;
        public int Amount;
    }
}