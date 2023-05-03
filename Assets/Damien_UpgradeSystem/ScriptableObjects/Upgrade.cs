using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damien.UpgradeSystem.ScriptableObjects
{
    public class Upgrade : UpgradeSystemEntity
    {
        public Dictionary<Statistic, int> Statistics = new Dictionary<Statistic, int>();
    }
}