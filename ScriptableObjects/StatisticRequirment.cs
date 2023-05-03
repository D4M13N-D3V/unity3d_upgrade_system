using Damien.UpgradeSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Damien.UpgradeSystem.ScriptableObjects
{
    [System.Serializable]
    internal class StatisticRequirment : Requirment
    {
        [SerializeField]
        public Statistic Statistic;
    }
}