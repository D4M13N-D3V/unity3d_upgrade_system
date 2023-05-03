using Damien.UpgradeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public UpgradeSystem UpgradeSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var upgrades = UpgradeSystem.GetAllUpgrades();
        var stats = UpgradeSystem.GetStatistics();
    }
}
