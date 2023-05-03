using Damien.UpgradeSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Damien.UpgradeSystem
{
    [SerializeField]
    public class Upgrade
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public List<Statistic> Statistics;

        public Upgrade(string name, string description, Sprite icon, List<Statistic> statistics)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Statistics = statistics;
        }
    }
}