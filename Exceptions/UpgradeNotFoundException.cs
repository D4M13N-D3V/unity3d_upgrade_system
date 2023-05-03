using Damien.UpgradeSystem.Interfaces;
using Damien.UpgradeSystem.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Damien.UpgradeSystem.Exceptions
{
    public class UpgradeNotFoundException : Exception
    {
        public string ProvidedUpgradeName;

        public UpgradeNotFoundException(string providedUpgradeName, string message) : base(message)
        {

        }
    }
}