using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damien.UpgradeSystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Statistic", menuName = "Damien/Upgrade System/Create Upgrade Requirment")]
    internal abstract class Requirment : ScriptableObject
    {
        public int Minimum;
        public int Maximum;
    }
}