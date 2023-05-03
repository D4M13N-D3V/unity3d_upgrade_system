using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damien.UpgradeSystem.ScriptableObjects
{
    public abstract class Requirment : ScriptableObject
    {
        public int Minimum;
        public int Maximum;
    }
}